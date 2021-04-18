using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class Game : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Context Context;
        // If not null, the game will be restarted after Update loop finishes
        private IWorldLoader willRestartTo;

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
                Graphics = graphics,
                Keyboard = new KeyboardManager(),
                Game = this
            };

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Context.SpriteBatch = spriteBatch;
            Context.Assets.LoadContent(Content);
            
            Entity player = CreatePlayer();
            Context.Player = (PlayerBehaviour)player.Behavior;

            StartLevel(new FirstLevelLoader());
        }

        private void StartLevel(IWorldLoader worldLoader) {
            Context.Score = new Score();
            
            Entity player = CreatePlayer();
            Context.Player = (PlayerBehaviour)player.Behavior;

            Context.World = worldLoader.LoadWorld(Context);
            Context.World.CurrentScene.AddEntity(player);

            Context.Camera.Target = player;
            Context.Camera.Update();

            Context.Renderer.RebuildTerrainMesh();
        }

        public void Restart(IWorldLoader newWorldLoader) {
            willRestartTo = newWorldLoader;
        }

        // Dont call this if you dont know what you are doing
        public void TriggerRestartCheck() {
            if (willRestartTo != null) {
                Context.World.Destroy();
                StartLevel(willRestartTo);
                willRestartTo = null;
            }
        }

        private Entity CreatePlayer() {
            Entity player = new Entity(Context);
            var bucketList = new BucketList(new List<BucketListItem>());
            bucketList.FillBucketList(true);
            player.Behavior = new PlayerBehaviour(true, bucketList, 1, player);
            player.Collider = new CircleCollider(player, true, .1f);
            player.Position = new Vector3(25, 16, 0);
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
