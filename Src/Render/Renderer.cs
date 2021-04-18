using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace yolo {
    public class Renderer {
        private Context context;

        private QuadBuffer terrainMesh;

        public Renderer(Context context) {
            this.context = context;
            terrainMesh = new QuadBuffer();
        }

        public void RebuildTerrainMesh() {

            terrainMesh.Dispose();
            terrainMesh = new QuadBuffer();
            var tiles = context.World.CurrentScene.Tiles;

            for (int y = 0; y < tiles.Height; y++) {
                for (int x = 0; x < tiles.Width; x++) {

                    Tile t = tiles[x, y];
                    float height = t.Height;

                    if (t.Flat) {
                        terrainMesh.AddQuad(
                            new(new(x, y, 0), (-Vector3.UnitZ).ToColor(), t.Sprite.UVTopLeft),
                            new(new(x + 1, y, 0), (-Vector3.UnitZ).ToColor(), t.Sprite.UVTopRight),
                            new(new(x, y + 1, 0), (-Vector3.UnitZ).ToColor(), t.Sprite.UVBotLeft),
                            new(new(x + 1, y + 1, 0), (-Vector3.UnitZ).ToColor(), t.Sprite.UVBotRight)
                            );
                    } else {
                        // front facing quad
                        if (y == tiles.Height - 1 || tiles[x, y + 1].Height < height) {
                            terrainMesh.AddQuad(
                                new(new(x, y + 1, -height), (Vector3.UnitY).ToColor(), t.Sprite.UVTopLeft),
                                new(new(x + 1, y + 1, -height), (Vector3.UnitY).ToColor(), t.Sprite.UVTopRight),
                                new(new(x, y + 1, 0), (Vector3.UnitY).ToColor(), t.Sprite.UVBotLeft),
                                new(new(x + 1, y + 1, 0), (Vector3.UnitY).ToColor(), t.Sprite.UVBotRight)
                            );
                        }
                        // right facing quad
                        if (x == tiles.Width - 1 || tiles[x + 1, y].Height < height) {
                            terrainMesh.AddQuad(
                                new(new(x + 1, y + 1, -height), (Vector3.UnitX).ToColor(), t.Sprite.UVTopLeft),
                                new(new(x + 1, y, -height), (Vector3.UnitX).ToColor(), t.Sprite.UVTopRight),
                                new(new(x + 1, y + 1, 0), (Vector3.UnitX).ToColor(), t.Sprite.UVBotLeft),
                                new(new(x + 1, y, 0), (Vector3.UnitX).ToColor(), t.Sprite.UVBotRight)
                            );
                        }
                        // left facing quad
                        if (x == 0 || tiles[x - 1, y].Height < height) {
                            terrainMesh.AddQuad(
                                new(new(x, y, -height), (-Vector3.UnitX).ToColor(), t.Sprite.UVTopLeft),
                                new(new(x, y + 1, -height), (-Vector3.UnitX).ToColor(), t.Sprite.UVTopRight),
                                new(new(x, y, 0), (-Vector3.UnitX).ToColor(), t.Sprite.UVBotLeft),
                                new(new(x, y + 1, 0), (-Vector3.UnitX).ToColor(), t.Sprite.UVBotRight)
                            );
                        }

                        var UVOnePixel = new Vector2(0, 1) / t.Sprite.Texture.Height;

                        // top facing quad
                        terrainMesh.AddQuad(
                            new(new(x, y, -height), (-Vector3.UnitZ).ToColor(), t.Sprite.UVTopLeft),
                            new(new(x + 1, y, -height), (-Vector3.UnitZ).ToColor(), t.Sprite.UVTopRight),
                            new(new(x, y + 1, -height), (-Vector3.UnitZ).ToColor(), t.Sprite.UVTopLeft + UVOnePixel),
                            new(new(x + 1, y + 1, -height), (-Vector3.UnitZ).ToColor(), t.Sprite.UVTopRight + UVOnePixel)
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


            var shader = context.Assets.Perspective;

            var viewProjection = camera.View * projection;
            shader.CurrentTechnique = shader.Techniques["Terrain"];
            shader.Parameters["view_projection"].SetValue(viewProjection);
            shader.Parameters["SpriteTexture"].SetValue(context.Assets.Textures.Main);

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

            foreach (var pass in shader.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, terrainMesh.IndexBuffer.IndexCount / 3);
            }

            foreach (var entity in scene.Entities.Where(e => e.Animation != null).OrderBy(e => (e.Animation.IsFlat ? 0f : 1f, e.Position.Y))) {

                var sb = context.SpriteBatch;
                var anim = entity.Animation;
                var sprite = anim.GetCurrentSprite(context);

                device.SetRenderTarget(null);
                using var entityMesh = new QuadBuffer();
                entityMesh.AddSprite(sprite, entity.Position, anim.IsFlat);

                entityMesh.Transfer(device);
                device.SetVertexBuffer(entityMesh.VertexBuffer);
                device.Indices = entityMesh.IndexBuffer;

                shader.Parameters["intensity"].SetValue(1f);

                if (anim.Highlighted) {
                    shader.Parameters["intensity"].SetValue(2f);
                }

                shader.CurrentTechnique = shader.Techniques["Entity"];
                shader.Parameters["view_projection"].SetValue(viewProjection);
                shader.Parameters["tone"].SetValue(sprite.Tone.ToVector4());
                shader.Parameters["SpriteTexture"].SetValue(sprite.Texture);

                foreach (var pass in shader.CurrentTechnique.Passes) {
                    pass.Apply();
                    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, entityMesh.IndexBuffer.IndexCount / 3);
                }

            }
        }
    }
}