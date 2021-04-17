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
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Context.Update(gameTime);

            // TODO: Add your update logic here
            Context.Camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            /*Matrix view = Matrix.Identity;

            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, width, height, 0, 0, 1);

            var _firstShader = Context.Assets.Perspective;
            var _image = Context.Assets.Textures.Main;
            _firstShader.Parameters["view_projection"].SetValue(view * projection);

            spriteBatch.Begin(effect: _firstShader);
            spriteBatch.Draw(_image, new Vector2(0, 0), Color.White);
            spriteBatch.End();*/

            // TODO: Add your drawing code here
            Context.Renderer.Draw();

            base.Draw(gameTime);
        }
    }
}
