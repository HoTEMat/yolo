using System;
using Microsoft.Xna.Framework;

namespace yolo {
    public class FloatRandom {
        private Random rnd;

        public FloatRandom(Random rnd) => this.rnd = rnd;
        
        public Vector3 NextVector(Vector3 min, Vector3 max) {
            return new Vector3(
                x: NextFloat(min.X, max.X),
                y: NextFloat(min.Y, max.Y),
                z: NextFloat(min.Z, max.Z)
            );
        }

        public float NextFloat(float min, float max) {
            float res = (float) (min + (max - min) * rnd.NextDouble());
            return res;
        }
    }
}