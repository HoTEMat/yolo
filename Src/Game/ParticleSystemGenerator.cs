using Microsoft.Xna.Framework;

namespace yolo {
    public class ParticleSystemGenerator {
        private Scene scene;
        private Context ctx;
        public int Count { get; init; }
        public IInterpolation<float> ScaleInterpolation { get;init; }
        public IInterpolation<Vector3> VelocityInterpolation { get; init; }
        // In seconds
        public int Duration { get; init; }
        public float RectWidth { get; init; }

        public ParticleSystemGenerator(Scene scene, Context ctx) {
            this.scene = scene;
            this.ctx = ctx;
        }

        public static ParticleSystemGenerator HearthsGenerator(Scene scene, Context ctx) {
            return new ParticleSystemGenerator(scene, ctx) {
                Count = 5,
                ScaleInterpolation = new LinearInterpolation(1, 0.5f),
                VelocityInterpolation = new VectorInterpolation(
                    xInterpolation: new Constant<float>(0),
                    yInterpolation: new Constant<float>(0),
                    zInterpolation: new LinearInterpolation(-0.5f, 0)
                ),
                Duration = 2,
                RectWidth = 1
            };
        }

        public void Generate(ISpriteSet sprite, Vector3 position) {
            float hw = RectWidth / 2;
            Vector3 leftTopBound = position + World.Backward * hw + World.Left * hw;
            Vector3 rightBottomBound = position + World.Forward * hw + World.Right * hw;

            for (int i = 0; i < Count; i++) {
                Vector3 pos = ctx.FloatRandom.NextVector(leftTopBound, rightBottomBound);
                AddSprite(sprite, pos);
            }
        }

        private void AddSprite(ISpriteSet sprite, Vector3 position) {
            Entity particle = new Entity(ctx) {
                Animation = new Animation(sprite),
                Position = position
            };
            Particle behaviour = new Particle(particle, ScaleInterpolation, VelocityInterpolation, Duration);
            particle.Behavior = behaviour;
            scene.AddEntity(particle);
            behaviour.StartAnimating();
        }
    }
}