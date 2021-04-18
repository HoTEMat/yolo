using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class Entity {
        public Vector3 Position { get; set; }
        public Scene Scene { get; set; }
        public IAnimation Animation { get; set; }
        public ICollider Collider { get; set; }
        public Behaviour Behavior { get; set; }
        public Context Context { get; set; }
        public bool Destroyed { get; private set; }
        public bool IsTemporal { get; init; }

        public Entity(Context ctx) {
            Context = ctx;
        }

        public void ChangeSpriteTo(ISpriteSet spriteSet) {
            if (Animation == null) {
                Animation = new Animation(spriteSet);
            } else {
                Animation.Reset(spriteSet);
            }
        }

        public void Update() {
            Animation?.Update(Context);
            Behavior?.Update();
        }

        public void Destroy() {
            if (Animation != null) {
                Animation.Dispose();
            }
            Destroyed = true;
        }

        public float GetDistanceFrom(Entity other) {
            // Distance between centers
            float centerDst = (other.Position - Position).Length();
            // Distance from our center to other's border
            float dstFromOtherBorder = other.Collider?.DistanceFromBorderTo(Position) ?? centerDst;
            // Distance from other's center to our border
            float dstToOurBorder = Collider?.DistanceFromBorderTo(other.Position) ?? centerDst;
            return dstFromOtherBorder + dstToOurBorder - centerDst;
        }
    }
}