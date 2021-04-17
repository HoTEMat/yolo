using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace yolo {
    public class Renderer {
        private Context context;

        private QuadBuffer terrainMesh;
        private QuadBuffer entityMesh;

        public Renderer(Context context) {
            this.context = context;
            entityMesh = new();
            terrainMesh = new QuadBuffer();
        }

        public void RebuildTerrainMesh() {

            terrainMesh.Dispose();
            terrainMesh = new QuadBuffer();
            var tiles = context.World.CurrentScene.Tiles;



            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {

                    Tile t = tiles[x, y];

                    if (t.Flat) {
                        terrainMesh.AddQuad(
                            new(new(x, y, 0), -Vector3.UnitZ, t.Sprite.UVTopLeft),
                            new(new(x + 1, y, 0), -Vector3.UnitZ, t.Sprite.UVTopRight),
                            new(new(x, y + 1, 0), -Vector3.UnitZ, t.Sprite.UVBotLeft),
                            new(new(x + 1, y + 1, 0), -Vector3.UnitZ, t.Sprite.UVBotRight)
                            );
                    } else {
                        float height = t.Sprite.SourceRect.Height / 16f;

                        // front facing quad
                        if (y == tiles.Height - 1 || tiles[x, y + 1].Flat) {
                            terrainMesh.AddQuad(
                                new(new(x, y + 1, -height), Vector3.UnitY, t.Sprite.UVTopLeft),
                                new(new(x + 1, y + 1, -height), Vector3.UnitY, t.Sprite.UVTopRight),
                                new(new(x, y + 1, 0), Vector3.UnitY, t.Sprite.UVBotLeft),
                                new(new(x + 1, y + 1, 0), Vector3.UnitY, t.Sprite.UVBotRight)
                            );
                        }
                        // right facing quad
                        if (x == tiles.Width - 1 || tiles[x + 1, y].Flat) {
                            terrainMesh.AddQuad(
                                new(new(x + 1, y + 1, -height), Vector3.UnitX, t.Sprite.UVTopLeft),
                                new(new(x + 1, y, -height), Vector3.UnitX, t.Sprite.UVTopRight),
                                new(new(x + 1, y + 1, 0), Vector3.UnitX, t.Sprite.UVBotLeft),
                                new(new(x + 1, y, 0), Vector3.UnitX, t.Sprite.UVBotRight)
                            );
                        }
                        // left facing quad
                        if (x == 0 || tiles[x - 1, y].Flat) {
                            terrainMesh.AddQuad(
                                new(new(x, y, -height), Vector3.UnitX, t.Sprite.UVTopLeft),
                                new(new(x, y + 1, -height), Vector3.UnitX, t.Sprite.UVTopRight),
                                new(new(x, y, 0), Vector3.UnitX, t.Sprite.UVBotLeft),
                                new(new(x, y + 1, 0), Vector3.UnitX, t.Sprite.UVBotRight)
                            );
                        }

                        var UVOnePixel = new Vector2(0, 1) / t.Sprite.Texture.Height;

                        // top facing quad
                        terrainMesh.AddQuad(
                            new(new(x, y, -2), -Vector3.UnitZ, t.Sprite.UVTopLeft),
                            new(new(x + 1, y, -2), -Vector3.UnitZ, t.Sprite.UVTopRight),
                            new(new(x, y + 1, -2), -Vector3.UnitZ, t.Sprite.UVTopLeft + UVOnePixel),
                            new(new(x + 1, y + 1, -2), -Vector3.UnitZ, t.Sprite.UVTopRight + UVOnePixel)
                        );
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
            var scene = context.World.CurrentScene;

            device.Clear(Color.CornflowerBlue);

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
                DepthBufferFunction = CompareFunction.LessEqual,
            };

            device.BlendState = BlendState.AlphaBlend;
            device.DepthStencilState = DepthStencilState.Default;

            foreach (var pass in persp.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, terrainMesh.IndexBuffer.IndexCount / 3);
            }

            entityMesh.Dispose();
            entityMesh = new QuadBuffer();
            foreach (var entity in scene.Entities.OrderBy(e => e.Position.Y)) {
                if (entity.Animation != null) {
                    var sprite = entity.Animation.GetCurrentSprite();

                    entityMesh.AddSprite(sprite, entity.Position);
                }
            }

            entityMesh.Transfer(device);
            device.SetVertexBuffer(entityMesh.VertexBuffer);
            device.Indices = entityMesh.IndexBuffer;

            foreach (var pass in persp.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, entityMesh.IndexBuffer.IndexCount / 3);
            }
        }
    }
}