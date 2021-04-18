using System.Collections.Generic;
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

            Entity player = CreatePlayer();
            player.Position = new Vector3(25, 16, 0);
            Context.Player = (PlayerBehaviour)player.Behavior;
            Context.World.CurrentScene.AddEntity(player);

            Context.Camera.Target = player;
            Context.Camera.Update();

            Context.Renderer.RebuildTerrainMesh();
        }

        private Entity CreatePlayer() {
            Entity player = new Entity(Context);
            player.Behavior = new PlayerBehaviour(true, new BucketList(new List<BucketListItem>()), 1, player);
            player.Collider = new CircleCollider(player, true, .1f);
            return player;
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Context.Update(gameTime);
            Context.World.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            Context.Renderer.Draw();
            Context.Hud.Draw();

            base.Draw(gameTime);
        }
    }
}
