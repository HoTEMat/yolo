using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;

namespace yolo {
    public interface IInterpolation<out T> {
        T Interpolate(float t);
    }

    public class LinearInterpolation : IInterpolation<float> {
        public float Start { get; init; }
        public float End { get; init; }

        public LinearInterpolation(float start, float end) {
            Start = start;
            End = end;
        }
        
        public float Interpolate(float t) {
            return Start + (End - Start) * t;
        }
    }

    public class VectorInterpolation : IInterpolation<Vector3> {
        public IInterpolation<float> XInterpolation;
        public IInterpolation<float> YInterpolation;
        public IInterpolation<float> ZInterpolation;

        public VectorInterpolation(IInterpolation<float> xInterpolation, IInterpolation<float> yInterpolation, IInterpolation<float> zInterpolation) {
            XInterpolation = xInterpolation;
            YInterpolation = yInterpolation;
            ZInterpolation = zInterpolation;
        }

        public Vector3 Interpolate(float t) {
            return new(XInterpolation.Interpolate(t), YInterpolation.Interpolate(t), ZInterpolation.Interpolate(t));
        }
    }

    public class Constant<T> : IInterpolation<T> {
        public T Value { get; init; }
        public Constant(T value) => Value = value;
        public T Interpolate(float t) => Value;
    }
}