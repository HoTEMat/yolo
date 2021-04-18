using System;
using System.Collections.Generic;

namespace yolo
{
    public class Utils
    {
        public static Random Random = new Random();
        public static T RandChoice<T>(IList<T> list)
        {
            int idx = Random.Next(0, list.Count);
            return list[idx];
        }

        public static T[] RandChooseN<T>(T[] array, int N)
        {
            if (N > array.Length)
            {
                throw new IndexOutOfRangeException();
            }
            T[] res = new T[N];
            var usedIndices = new List<int>();
            for (int i = 0; i < N; i++)
            {
                int idx = Random.Next(0, array.Length);
                while (usedIndices.Contains(idx))
                {
                    idx = Random.Next(0, array.Length);
                }
                usedIndices.Add(idx);
                res[i] = array[idx];
            }
            return res;
        }
    }
}