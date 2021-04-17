using Microsoft.Xna.Framework;

namespace yolo {
    public class Entity {
        public Vector2 Position { get; set; }
        public Scene Scene { get; set; }
        public Animation Animation { get; private set; }
        public ICollider Collider { get; init; }
        public Behaviour Behavior { get; init; }
        public Context Context { get; }
        public bool Destroyed { get; private set; }

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
            Destroyed = true;
        }
    }
}