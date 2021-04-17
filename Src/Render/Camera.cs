using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class Camera {

        private Context context;

        public Camera(Context context) {
            this.context = context;
        }

        public Vector3 Center = new Vector3(0, 0, 0);
        public Vector3 EyeOffset { get; set; } = new Vector3(0, 5, -5);
        public Entity Target { get; set; }

        public Matrix View { get; private set; }

        public void Update() {
            var kbs = Keyboard.GetState();

            float speed = 0.1f;
            /*
            if (kbs.IsKeyDown(Keys.Right)) {
                Center += new Vector3(speed, 0, 0);
            }
            if (kbs.IsKeyDown(Keys.Left)) {
                Center += new Vector3(-speed, 0, 0);
            }
            if (kbs.IsKeyDown(Keys.Up)) {
                Center += new Vector3(0, -speed, 0);
            }
            if (kbs.IsKeyDown(Keys.Down)) {
                Center += new Vector3(0, speed, 0);
            }/*/

            if (Target != null) {
                Center = Target.Position;
            }

            if (kbs.IsKeyDown(Keys.Add)) {
                EyeOffset += new Vector3(0, 0, speed);
            }
            if (kbs.IsKeyDown(Keys.Subtract)) {
                EyeOffset += new Vector3(0, 0, -speed);
            }

            var viewport = context.Graphics.GraphicsDevice.Viewport;

            View = Matrix.Identity;
            View *= Matrix.CreateLookAt(Center + EyeOffset, Center * 1, -Vector3.UnitZ);
        }
    }
}