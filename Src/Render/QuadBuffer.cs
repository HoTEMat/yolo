using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yolo {
    class QuadBuffer<T> where T : struct {
        public VertexBuffer VertexBuffer { get; private set; } = null;
        public IndexBuffer IndexBuffer { get; private set; } = null;

        List<int> indices = new List<int>();
        List<T> vertices = new List<T>();

        public void AddQuad(T topLeft, T topRight, T botLeft, T botRigth) {
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

        public void Transfer(GraphicsDevice device) {
            VertexBuffer = new VertexBuffer(device, typeof(T), vertices.Count, BufferUsage.WriteOnly);
            IndexBuffer = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, indices.Count, BufferUsage.WriteOnly);

            VertexBuffer.SetData(vertices.ToArray());
            IndexBuffer.SetData(indices.ToArray());

            //vertices.Clear();
            //indices.Clear();
        }
    }
}
