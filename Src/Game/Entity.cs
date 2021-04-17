using Microsoft.Xna.Framework;

namespace yolo {
    public class Entity {
        public Vector2 Position { get; }
        public Scene Scene { get; }
        public Animation Animation { get; }
        public ICollider Collider { get;}
        public IBehaviour Behavior { get; }
    }
}