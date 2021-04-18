using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class Context {

        public Context() {
            Assets = new AssetBank();
            Camera = new Camera(this);
            Renderer = new Renderer(this);
            Hud = new HUD(this);
            Random = new Random();
            FloatRandom = new FloatRandom(Random);
        }

        /// <summary>
        /// Delta time in Seconds
        /// </summary>
        public float dSec => (float)GameTime.ElapsedGameTime.TotalSeconds;
        /// <summary>
        /// Total time in Seconds
        /// </summary>
        public float TSec => (float)GameTime.TotalGameTime.TotalSeconds;

        public GraphicsDeviceManager Graphics { get; init; }
        public KeyboardManager Keyboard { get; init; }
        public GameTime GameTime { get; private set; }
        public AssetBank Assets { get; private set; }
        public Game Game { get; init; }
        public World World { get; set; }
        public PlayerBehaviour Player { get; set; }
        public Camera Camera { get; private set; }
        public Renderer Renderer { get; private set; }
        public HUD Hud { get; private set; }
        
        public Score Score { get; set; }

        public Random Random { get; }
        public FloatRandom FloatRandom { get; }
        public SpriteBatch SpriteBatch { get; set; }

        internal void Update(GameTime gameTime) {
            this.GameTime = gameTime;
        }
    }
}