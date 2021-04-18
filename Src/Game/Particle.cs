using Microsoft.Xna.Framework;

namespace yolo {
    public class Particle : Behaviour {
        public Vector3 Velocity { get; set; }

        public IInterpolation<float> ScaleInterpolation { get; init; }
        public IInterpolation<Vector3> VelocityInterpolation { get; init; }
        public float Duration { get; init; }
        private float RunningTime = 0;

        public Particle(Entity entity, IInterpolation<float> scaleInterpolation,
            IInterpolation<Vector3> velocityInterpolation, float duration) : base(entity) {
            ScaleInterpolation = scaleInterpolation;
            VelocityInterpolation = velocityInterpolation;
            Duration = duration;
        }

        public void StartAnimating() {
            RunningTime = 0;
        }

        public override void Update() {
            float deltaTime = (float) Context.GameTime.ElapsedGameTime.TotalSeconds;
            RunningTime += deltaTime;
            
            UpdateInterpolations();
            UpdatePosition(deltaTime);
        }

        private void UpdateInterpolations() {
            float t = RunningTime / Duration;
            if (t > 1) {
                Entity.Destroy();
            } else {
                Velocity = VelocityInterpolation.Interpolate(t);
                Entity.Scale = ScaleInterpolation.Interpolate(t);
            }
        }

        private void UpdatePosition(float deltaTime) {
            Entity.Position += Velocity * deltaTime;
        }
    }
}