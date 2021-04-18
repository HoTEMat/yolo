using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public abstract class PopupWindow {
        protected Context context;
        protected AssetBank assets => context.Assets;

        public PopupWindow(Context context) {
            this.context = context;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
    }

    public class InfoWindow : PopupWindow {
        public InfoWindow(Context context) : base(context) {
        }

        public override void Update(GameTime gameTime) {
            if (context.Keyboard.IsKeyPressed(Keys.F)) {
                context.Game.InfoScreenDone();
            }
        }

        public override void Draw() {
            var device = context.Graphics.GraphicsDevice;
            var viewport = context.Graphics.GraphicsDevice.Viewport;
            SpriteBatch sb = context.SpriteBatch;
            Rectangle bounds = viewport.Bounds;

            sb.Begin(samplerState: SamplerState.PointClamp);

            sb.DrawStringCentered("Press F to interact. Come on, try it.", new Vector2(bounds.Width / 2f, bounds.Height / 3f), 4, Color.Black);
            
            sb.End();
        }
    }

    public class IntroWindow : PopupWindow {
        private int selectedButton;
        private const int buttonsCount = 2;
        public IntroWindow(Context context) : base(context) {
        }

        public override void Update(GameTime gameTime) {
            if (context.Keyboard.IsKeyPressed(Keys.Left)) {
                selectedButton--;
                if (selectedButton < 0) {
                    selectedButton = buttonsCount - 1;
                }
            } else if (context.Keyboard.IsKeyPressed(Keys.Right)) {
                selectedButton = (selectedButton + 1) % buttonsCount;
            }

            bool good;
            if (context.Keyboard.IsKeyPressed(Keys.G)) {
                good = true;
            } else if (context.Keyboard.IsKeyPressed(Keys.B)) {
                good = false;
            } else return;
            
            context.Game.EndIntro(good);
            //context.Game.EndIntro(true); // TODO
        }

        public override void Draw() {
            var device = context.Graphics.GraphicsDevice;
            var viewport = context.Graphics.GraphicsDevice.Viewport;
            SpriteBatch sb = context.SpriteBatch;
            Rectangle bounds = viewport.Bounds;
            
            sb.Begin(samplerState: SamplerState.PointClamp);
            DrawTransparentBackground(sb, bounds);
            sb.FillRectangle(new Rectangle((int) (bounds.Width / 2f - 750), (int) (bounds.Height / 2f - 300), 1500, 350), Color.Black);
            sb.DrawStringCentered("You have been diagnosed with a terminal illness.", new Vector2(bounds.Width / 2f, bounds.Height / 3f), 4, Color.White);
            sb.DrawStringCentered("Are you (G)ood or (B)ad?", new Vector2(bounds.Width / 2f, bounds.Height / 3f + 50), 4, Color.White);
            
            
            sb.End();
        }

        private void DrawTransparentBackground(SpriteBatch sb, Rectangle screenBounds) {
            sb.FillRectangle(screenBounds, new Color(Color.Black, 0.5f));
        }
    }

    public class OutroWindow : PopupWindow
    {
        public OutroWindow(Context context) : base(context)
        {
        }
        public override void Update(GameTime gameTime)
        {
            if (context.Keyboard.IsKeyDown(Keys.Enter))
            {
                context.World?.Destroy();
                context.Game.StartGame();
            }
        }
        public override void Draw()
        {
            SpriteBatch sb = context.SpriteBatch;
            var viewport = context.Graphics.GraphicsDevice.Viewport;
            sb.Begin(samplerState: SamplerState.PointClamp);
            sb.Draw(context.Assets.Sprites.Empty.Texture, 
                new Rectangle(0, 0, viewport.Width, viewport.Width), 
                context.Assets.Sprites.Empty.SourceRect, Color.White );
          
            sb.DrawStringCentered("You died..", new Vector2(viewport.Width/2, viewport.Height/6), 8, Color.White);
            
            sb.DrawStringCentered("Score: " + context.Score.Value, new Vector2(viewport.Width/2, viewport.Height/2), 4, Color.White);
            
            /*sb.Draw(context.Assets.Sprites.Paper.Texture, 
                new Rectangle(0, 300, viewport.Width, viewport.Width), 
                context.Assets.Sprites.Empty.SourceRect, Color.White );*/
            
            sb.DrawStringCentered("Play again", new Vector2(viewport.Width/2, 2 * viewport.Height/3), 6, Color.White);
            sb.DrawStringCentered("[Press enter]", new Vector2(viewport.Width/2, 5 * viewport.Height/6), 2, Color.White);
            sb.End();
        }
    }
}