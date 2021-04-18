using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class Game : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Context Context;
        private Controller currentController;
        // If not null, control will be handed to this controller
        public Controller WillTransitionTo { get; set; }

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
            DrawUtils.Font = Context.Assets.Fonts.Font;
            
            StartGame();
        }

        // Only call this once per game.
        public void StartGame() {
            // Hack: Player is good at first in the intro scene
            PlayingGameController controller = new PlayingGameController(Context, true);
            controller.StartLevel(new IntroLevelLoader());
            currentController = controller;
        }

        public void StartIntro() {
            // Don't destroy World.
            WillTransitionTo = new IntroScreenController(Context);
        }

        // Can be called by entities
        public void GameOver() {
            WillTransitionTo = new GameOverScreenController(Context);
        }

        public void EndIntro(bool isGood) {
            PlayingGameController controller = new PlayingGameController(Context, isGood);
            controller.StartLevel(new FirstLevelLoader());
            WillTransitionTo = controller;
        }

        public void ProZenu() {
            WillTransitionTo = new GameOverScreenController(Context);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Context.Update(gameTime);
            Context.Keyboard.RegisterState(Keyboard.GetState());
            CheckTransition();
            currentController.Update(gameTime);
            base.Update(gameTime);
        }

        private void CheckTransition() {
            if (WillTransitionTo != null) {
                currentController = WillTransitionTo;
                WillTransitionTo = null;

                if (currentController is IntroScreenController) {
                    Context.World.CurrentScene.RemoveTemporalEntitiesNow();
                }
            }
        }

        protected override void Draw(GameTime gameTime) {
            currentController.Draw(gameTime);
            base.Draw(gameTime);
        }
    }

    public abstract class Controller {
        protected Context ctx;

        protected Controller(Context ctx) {
            this.ctx = ctx;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }

    public class PlayingGameController : Controller {
        private bool playerIsGood;
        public PlayingGameController(Context ctx, bool playerIsGood) : base(ctx) {
            this.playerIsGood = playerIsGood;
        }

        public override void Update(GameTime gameTime) {
            ctx.Update(gameTime);
            ctx.World.Update();
        }

        public override void Draw(GameTime gameTime) {
            ctx.Renderer.Draw();
            ctx.Hud.Draw();
        }
        
        public void StartLevel(IWorldLoader worldLoader) {
            ctx.Score = new Score();
            
            Entity player = CreatePlayer(worldLoader.PlayerStartVector);
            ctx.Player = (PlayerBehaviour)player.Behavior;

            ctx.World = worldLoader.LoadWorld(ctx);
            ctx.World.CurrentScene.AddEntity(player);

            ctx.Camera.Target = player;
            ctx.Camera.Update();

            ctx.Renderer.RebuildTerrainMesh();
        }
        
        private Entity CreatePlayer(Vector3 pos) {
            Entity player = new Entity(ctx);
            var bucketList = new BucketList(new List<BucketListItem>());
            bucketList.FillBucketList(true);
            player.Behavior = new PlayerBehaviour(playerIsGood, bucketList, 1, player);
            player.Collider = new CircleCollider(player, true, .1f);
            player.Position = pos;
            return player;
        }
    }

    public class IntroScreenController : Controller {
        private IntroWindow introWindow;
        public IntroScreenController(Context ctx) : base(ctx) {
            introWindow = new IntroWindow(ctx);
        }

        public override void Update(GameTime gameTime) {
            introWindow.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            introWindow.Draw();
        }

        // TODO: call this
        private void HandleChosen(bool isGood) {
            PlayingGameController newController = new PlayingGameController(ctx, isGood);
            newController.StartLevel(new FirstLevelLoader());
            ctx.Game.WillTransitionTo = newController;
        }
    }

    public class GameOverScreenController : Controller {
        private OutroWindow outroWindow;
        
        public GameOverScreenController(Context ctx) : base(ctx) {
            outroWindow = new OutroWindow(ctx);
        }

        public override void Update(GameTime gameTime) {
            outroWindow.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            outroWindow.Draw(gameTime);
        }

        
        // TODO: call this
        private void HandleRestart() {
            ctx.World.Destroy();
            ctx.Game.StartGame();
        }
    }
}
