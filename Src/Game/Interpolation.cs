using System;
using Microsoft.Xna.Framework;

namespace yolo {
    public interface IInterpolation<out T> {
        T Interpolate(float t);
    }

    public class LinearInterpolation : IInterpolation<float> {
        public float Start { get; init; }
        public float End { get; init; }
        
        public float Interpolate(float t) {
            return Start + (End - Start) * t;
        }
    }

    public class VectorInterpolation : IInterpolation<Vector2> {
        public IInterpolation<float> XInterpolation;
        public IInterpolation<float> YInterpolation;
        
        public Vector2 Interpolate(float t) {
            return new(XInterpolation.Interpolate(t), YInterpolation.Interpolate(t));
        }
    }
}