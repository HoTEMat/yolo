using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using T = Microsoft.Xna.Framework.Graphics.VertexPositionColorTexture;

namespace yolo {
    class QuadBuffer : IDisposable {
        public VertexBuffer VertexBuffer { get; private set; }
        public IndexBuffer IndexBuffer { get; private set; }
        public bool IsDisposed { get; private set; }

        List<int> indices = new List<int>();
        List<T> vertices = new List<T>();

        public void AddQuad(T topLeft, T topRight, T botLeft, T botRigth) {
            if (IsDisposed) {
                throw new InvalidOperationException("Object is already disposed");
            }
            if (VertexBuffer != null) {
                throw new InvalidOperationException("The buffers are already transfered");
            }

            int iTL = vertices.Count;
            vertices.Add(topLeft);
            int iTR = vertices.Count;
            vertices.Add(topRight);
            int iBL = vertices.Count;
            vertices.Add(botLeft);
            int iBR = vertices.Count;
            vertices.Add(botRigth);

            indices.Add(iTL);
            indices.Add(iTR);
            indices.Add(iBL);

            indices.Add(iTR);
            indices.Add(iBR);
            indices.Add(iBL);
        }

        void swap<T>(ref T a, ref T b) {
            T temp = a;
            a = b;
            b = temp;
        }
        public void AddSprite(Sprite sprite, Vector3 position, bool isFlat = false) {

            var quad = new Vector2[] {
                new(0,0), new (sprite.SourceRect.Width, 0),
                new(0,sprite.SourceRect.Height), new (sprite.SourceRect.Width, sprite.SourceRect.Height),
            };

            Matrix transform =
                Matrix.CreateTranslation(new(-sprite.Origin, 0)) *
                Matrix.CreateScale(1 / 16f * sprite.Scale);

            for (int i = 0; i < quad.Length; i++) {
                quad[i] = Vector2.Transform(quad[i], transform);
            }

            Vector2 UVTopLeft = sprite.UVTopLeft;
            Vector2 UVTopRight = sprite.UVTopRight;
            Vector2 UVBotLeft = sprite.UVBotLeft;
            Vector2 UVBotRight = sprite.UVBotRight;

            if ((sprite.Effects & SpriteEffects.FlipHorizontally) != 0) {
                swap(ref UVTopLeft, ref UVTopRight);
                swap(ref UVBotLeft, ref UVBotRight);
            }
            if ((sprite.Effects & SpriteEffects.FlipVertically) != 0) {
                swap(ref UVTopLeft, ref UVBotLeft);
                swap(ref UVTopRight, ref UVBotRight);
            }

            if (!isFlat) {

                AddQuad(
                    new T(new Vector3(quad[0].X, 0, quad[0].Y) + position, Vector3.UnitY.ToColor(), UVTopLeft),
                    new T(new Vector3(quad[1].X, 0, quad[1].Y) + position, Vector3.UnitY.ToColor(), UVTopRight),
                    new T(new Vector3(quad[2].X, 0, quad[2].Y) + position, Vector3.UnitY.ToColor(), UVBotLeft),
                    new T(new Vector3(quad[3].X, 0, quad[3].Y) + position, Vector3.UnitY.ToColor(), UVBotRight)
                );

            } else {

                AddQuad(
                    new T(new Vector3(quad[0].X, quad[0].Y, -0.01f) + position, Vector3.UnitY.ToColor(), UVTopLeft),
                    new T(new Vector3(quad[1].X, quad[1].Y, -0.01f) + position, Vector3.UnitY.ToColor(), UVTopRight),
                    new T(new Vector3(quad[2].X, quad[2].Y, 0) + position, Vector3.UnitY.ToColor(), UVBotLeft),
                    new T(new Vector3(quad[3].X, quad[3].Y, 0) + position, Vector3.UnitY.ToColor(), UVBotRight)
                );
            }
        }

        public void Transfer(GraphicsDevice device) {
            VertexBuffer = new VertexBuffer(device, typeof(T), vertices.Count, BufferUsage.WriteOnly);
            IndexBuffer = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, indices.Count, BufferUsage.WriteOnly);

            VertexBuffer.SetData(vertices.ToArray());
            IndexBuffer.SetData(indices.ToArray());

            vertices.Clear();
            indices.Clear();
        }

        public void Dispose() {
            if (VertexBuffer != null) {
                VertexBuffer.Dispose();
            }

            if (IndexBuffer != null) {
                IndexBuffer.Dispose();
            }

            VertexBuffer = null;
            IndexBuffer = null;
            IsDisposed = true;
        }
    }
}
