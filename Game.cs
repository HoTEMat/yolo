using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class Game : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Context Context;

        public Game() {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            Context = new Context {
                Graphics = graphics
            };

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Context.SpriteBatch = spriteBatch;
            Context.Assets.LoadContent(Content);

            IWorldLoader worldLoader = new FirstLevelLoader();
            Context.World = worldLoader.LoadWorld(Context);
            Context.World.SwitchToScene("main", new Vector2(0, 0));
            Context.Camera.Center = new(Context.World.CurrentScene.Tiles.Size / 2f, 0);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Context.Update(gameTime);
            Context.Camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            Context.Renderer.Draw();

            base.Draw(gameTime);
        }
    }
}
