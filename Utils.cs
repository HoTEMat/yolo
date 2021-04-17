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
    }
}