using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class Context {

        public Context() {
            Assets = new AssetBank();
            Camera = new Camera(this);
            Renderer = new Renderer(this);
            Random = new Random();
        }

        public GraphicsDeviceManager Graphics { get; init; }
        public GameTime GameTime { get; private set; }
        public AssetBank Assets { get; private set; }
        public World World { get; set; }
        public PlayerBehaviour Player { get; }
        public Camera Camera { get; private set; }
        public Renderer Renderer { get; private set; }

        public Random Random { get; }
        public SpriteBatch SpriteBatch { get; set; }

        internal void Update(GameTime gameTime) {
            this.GameTime = gameTime;
        }
    }
}