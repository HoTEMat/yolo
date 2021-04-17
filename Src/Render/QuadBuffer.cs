using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using T = Microsoft.Xna.Framework.Graphics.VertexPositionNormalTexture;

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

        public void AddSprite(Sprite sprite, Vector3 position, bool isFlat = false) {

            Vector2 wsSize = sprite.SourceRect.Size.ToVector2() / 16f;
            Vector2 topLeft = sprite.Origin / 16f;
            Vector2 topRight = topLeft;
            topRight.X += wsSize.X;
            Vector2 botLeft = topLeft;
            botLeft.Y += wsSize.Y;
            Vector2 botRight = topLeft + wsSize;

            if (!isFlat) {

                AddQuad(
                    new T(new (topLeft.X, 0, -topLeft.Y), Vector3.UnitY, sprite.UVTopLeft),
                    new T(new(topRight.X, 0, -topRight.Y), Vector3.UnitY, sprite.UVTopRight),
                    new T(new(botLeft.X, 0, -botLeft.Y), Vector3.UnitY, sprite.UVBotLeft),
                    new T(new(botRight.X, 0, -botRight.Y), Vector3.UnitY, sprite.UVBotRight)
                );

            } else {
                throw new NotImplementedException();
            }

        }

        public void Transfer(GraphicsDevice device) {
            VertexBuffer = new VertexBuffer(device, typeof(T), vertices.Count, BufferUsage.WriteOnly);
            IndexBuffer = new IndexBuffer(device, IndexElementSize.SixteenBits, indices.Count, BufferUsage.WriteOnly);

            VertexBuffer.SetData(vertices.ToArray());
            IndexBuffer.SetData(indices.ToArray());

            vertices.Clear();
            indices.Clear();
        }

        public void Dispose() {
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();

            VertexBuffer = null;
            IndexBuffer = null;
            IsDisposed = true;
        }
    }
}
