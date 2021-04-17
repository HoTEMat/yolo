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

        QuadBuffer<VertexPositionNormalTexture> terrainMesh;
        public void RebuildTerrainMesh() {

            terrainMesh = new QuadBuffer<VertexPositionNormalTexture>();
            var tiles = context.World.CurrentScene.Tiles;

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

            var device = context.Graphics.GraphicsDevice;
            var camera = context.Camera;
            var viewport = context.Graphics.GraphicsDevice.Viewport;

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45),
                (float)viewport.Width / viewport.Height,
                1 / 16f, 100f);


            var persp = context.Assets.Perspective;
            persp.CurrentTechnique = persp.Techniques["FloorPlane"];

            var viewProjection = camera.View * projection;
            persp.Parameters["view_projection"].SetValue(viewProjection);
            persp.Parameters["SpriteTexture"].SetValue(context.Assets.Textures.Main);

            device.SetVertexBuffer(terrainMesh.VertexBuffer);
            device.Indices = terrainMesh.IndexBuffer;

            device.RasterizerState = new() { CullMode = CullMode.CullCounterClockwiseFace };

            device.DepthStencilState = new() {
                DepthBufferEnable = true,
                DepthBufferWriteEnable = true,
                DepthBufferFunction = CompareFunction.Less,
            };

            foreach (var pass in persp.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, terrainMesh.IndexBuffer.IndexCount / 3);
            }
        }
    }
}