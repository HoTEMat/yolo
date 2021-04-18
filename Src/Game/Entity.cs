using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class Entity {
        public Vector3 Position { get; set; }
        public Scene Scene { get; set; }
        public Animation Animation { get; set; }
        public ICollider Collider { get; set; }
        public Behaviour Behavior { get; set; }
        public Context Context { get; set; }
        public bool Destroyed { get; private set; }
        public bool IsFlat { get; set; }
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float Scale { get; set; } = 1;

        public Entity(Context ctx) {
            Context = ctx;
        }

        public void ChangeSpriteTo(ISpriteSet spriteSet) {
            Animation = new Animation(spriteSet);
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
    }
}