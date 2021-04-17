using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yolo
{
    public class Indexer2D<T>
    {
        public T[] Array;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Rectangle Bounds { get { return new Rectangle(0, 0, Width, Height); } }

        public int Length { get { return Array.Length; } }

        public Vector2 Size => new Vector2(Width, Height);

        public Indexer2D(int width, int height)
        {
            Width = width;
            Height = height;

            Array = new T[width * height];
        }

        public Indexer2D(ICollection<T> collection, int width, int height)
        {
            Width = width;
            Height = height;

            Array = collection.ToArray();
        }

        public bool Contains(Point point)
        {
            return Bounds.Contains(point);
        }

        public T this[Point point]
        {
            get
            {
                if (Bounds.Contains(point)) return Array[point.X + point.Y * Width];
                else throw new IndexOutOfRangeException();
            }
            set
            {
                if (Bounds.Contains(point)) Array[point.X + point.Y * Width] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        public void Initialize(T value)
        {
            for (int i = 0; i < Length; i++)
                Array[i] = value;
        }
        public void Initialize(Func<T> initializer)
        {
            for (int i = 0; i < Length; i++)
                Array[i] = initializer.Invoke();
        }
        public void Initialize(Func<int, T> initializer)
        {
            for (int i = 0; i < Length; i++)
                Array[i] = initializer.Invoke(i);
        }
        public void Initialize(Func<int, int, T> initializer)
        {
            for (int i = 0; i < Length; i++)
                Array[i] = initializer.Invoke(i % Width, i / Width);
        }

        public T this[int x, int y]
        {
            get
            {
                if (Bounds.Contains(x, y)) return Array[x + y * Width];
                else throw new IndexOutOfRangeException();
            }
            set
            {
                if (Bounds.Contains(x, y)) Array[x + y * Width] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        public T this[int index]
        {
            get { return Array[index]; }
            set { Array[index] = value; }
        }
    }
}
