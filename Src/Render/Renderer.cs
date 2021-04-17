using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace yolo {
    public class Renderer {
        private Context context;

        public Renderer(Context context) {
            this.context = context;
        }

        Indexer2D<Tile> tiles = null;
        RenderTarget2D buffer;
        void Init() {
            if (tiles == null) {
                tiles = new Indexer2D<Tile>(100, 100);

                System.Random rnd = new();

                for (int i = 0; i < tiles.Length; i++) {
                    if (rnd.Next(5) == 1) {
                        tiles[i] = context.Assets.Tiles.Dev_Wall;
                    } else {
                        tiles[i] = context.Assets.Tiles.Dev_Floor;
                    }
                }

                BuildTerrainMesh();

                var viewport = context.Graphics.GraphicsDevice.Viewport;
                int scale = 1;
                buffer = new RenderTarget2D(context.Graphics.GraphicsDevice, viewport.Width / scale, viewport.Height / scale,
                    false, SurfaceFormat.Color, DepthFormat.Depth24);
            }
        }

        QuadBuffer<VertexPositionNormalTexture> terrainMesh;
        public void BuildTerrainMesh() {

            terrainMesh = new QuadBuffer<VertexPositionNormalTexture>();
            var tiles = this.tiles;

            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {

                    Tile t = tiles[x, y];

                    if (t.Walkable) {
                        terrainMesh.AddQuad(
                            new(new(x, y, 0), -Vector3.UnitZ, t.Sprite.UVTopLeft),
                            new(new(x + 1, y, 0), -Vector3.UnitZ, t.Sprite.UVTopRight),
                            new(new(x, y + 1, 0), -Vector3.UnitZ, t.Sprite.UVBotLeft),
                            new(new(x + 1, y + 1, 0), -Vector3.UnitZ, t.Sprite.UVBotRight)
                            );
                    } else {
                        // front facing quad
                        if (y == tiles.Height - 1 || tiles[x, y + 1].Walkable) {
                            terrainMesh.AddQuad(
                                new(new(x, y + 1, -2), Vector3.UnitY, t.Sprite.UVTopLeft),
                                new(new(x + 1, y + 1, -2), Vector3.UnitY, t.Sprite.UVTopRight),
                                new(new(x, y + 1, 0), Vector3.UnitY, t.Sprite.UVBotLeft),
                                new(new(x + 1, y + 1, 0), Vector3.UnitY, t.Sprite.UVBotRight)
                            );
                        }
                        // right facing quad
                        if (x == tiles.Width - 1 || tiles[x + 1, y].Walkable) {
                            terrainMesh.AddQuad(
                                new(new(x + 1, y + 1, -2), Vector3.UnitX, t.Sprite.UVTopLeft),
                                new(new(x + 1, y, -2), Vector3.UnitX, t.Sprite.UVTopRight),
                                new(new(x + 1, y + 1, 0), Vector3.UnitX, t.Sprite.UVBotLeft),
                                new(new(x + 1, y, 0), Vector3.UnitX, t.Sprite.UVBotRight)
                            );
                        }
                        // left facing quad
                        if (x == 0 || tiles[x - 1, y].Walkable) {
                            terrainMesh.AddQuad(
                                new(new(x, y, -2), Vector3.UnitX, t.Sprite.UVTopLeft),
                                new(new(x, y + 1, -2), Vector3.UnitX, t.Sprite.UVTopRight),
                                new(new(x, y, 0), Vector3.UnitX, t.Sprite.UVBotLeft),
                                new(new(x, y + 1, 0), Vector3.UnitX, t.Sprite.UVBotRight)
                            );
                        }
                    }
                }
            }

            // send buffers over to the graphics device
            terrainMesh.Transfer(context.Graphics.GraphicsDevice);
        }

        public void Draw() {
            Init();

            var device = context.Graphics.GraphicsDevice;
            var tiles = this.tiles;
            var camera = context.Camera;
            var viewport = context.Graphics.GraphicsDevice.Viewport;
            float time = MathF.Sin((float)context.GameTime.TotalGameTime.TotalSeconds);

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45),
                (float)viewport.Width / viewport.Height,
                1 / 16f, 100f);


            var persp = context.Assets.Perspective;
            persp.CurrentTechnique = persp.Techniques["FloorPlane"];

            //effect.Parameters["Depth"].SetValue(0f);
            var viewProjection = camera.View * projection;
            persp.Parameters["view_projection"].SetValue(viewProjection);
            persp.Parameters["SpriteTexture"].SetValue(context.Assets.Textures.Dev);

            device.SetVertexBuffer(terrainMesh.VertexBuffer);
            device.Indices = terrainMesh.IndexBuffer;

            device.RasterizerState = new() { CullMode = CullMode.CullCounterClockwiseFace };

            device.SetRenderTarget(buffer);
            device.DepthStencilState = new() {
                DepthBufferEnable = true,
                DepthBufferWriteEnable = true,
                DepthBufferFunction = CompareFunction.Less,
            };

            foreach (var pass in persp.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, terrainMesh.IndexBuffer.IndexCount / 3);
            }
            
            device.SetRenderTarget(null);
            var sb = context.SpriteBatch;
            sb.Begin(samplerState: SamplerState.PointClamp);
            sb.Draw(buffer, device.Viewport.Bounds, Color.White);
            sb.End();//*/
        }
    }
}