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
            player.Position = new Vector3(30, 30, 0);
            Context.Player = player.Behavior as PlayerBehaviour;
            Context.World.CurrentScene.AddEntity(player);
            
            Context.Camera.Center = Vector3.Zero; //new(Context.World.CurrentScene.Tiles.Size / 2f, 0);
            Context.Camera.Target = player;
            Context.Camera.Update();
            
            Context.Renderer.RebuildTerrainMesh();
        }

        private Entity CreatePlayer() {
            Entity player = new Entity(Context)
            {
                Position = new Vector3(20, 12, 0),
            };
            player.Behavior = new PlayerBehaviour(true, new BucketList(new List<BucketListItem>()), 1, player);
            player.Collider = new CircleCollider(player, false, .5f);
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
