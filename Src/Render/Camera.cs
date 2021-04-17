using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class Camera {

        private Context context;

        public Camera(Context context) {
            this.context = context;
        }

        public Vector2 Position;
        public Entity Target { get; set; }

        public void Update() {
            var kbs = Keyboard.GetState();

            float speed = 1f;
            if (kbs.IsKeyDown(Keys.Right)) {
                Position += new Vector2(speed,0);
            }
            if (kbs.IsKeyDown(Keys.Left)) {
                Position += new Vector2(-speed,0);
            }
            if (kbs.IsKeyDown(Keys.Up)) {
                Position += new Vector2(0, -speed);
            }
            if (kbs.IsKeyDown(Keys.Down)) {
                Position += new Vector2(0, speed);
            }
        }
    }
}