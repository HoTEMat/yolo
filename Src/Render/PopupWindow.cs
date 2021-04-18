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
            //context.Game.EndIntro(true); // TODO
        }

        public override void Draw() {
            var device = context.Graphics.GraphicsDevice;
            var viewport = context.Graphics.GraphicsDevice.Viewport;
            SpriteBatch sb = context.SpriteBatch;
            Rectangle bounds = viewport.Bounds;
            Console.WriteLine(bounds);
            
            sb.Begin(samplerState: SamplerState.PointClamp);
            DrawTransparentBackground(sb, bounds);
            sb.DrawStringCentered("Intro!!!!!", new Vector2(bounds.Width / 2f, bounds.Height / 3f), 5, Color.Blue); 
            
            sb.End();
        }

        private void DrawTransparentBackground(SpriteBatch sb, Rectangle screenBounds) {
            sb.FillRectangle(screenBounds, new Color(Color.Black, 1));
            //sb.DrawRectangle(new Rectangle(100, 100, 200, 200), Color.Black);
        }
        
        private void DrawButton(int x, int y) {
            
        }
    }

    public class OutroWindow : PopupWindow
    {
        public OutroWindow(Context context) : base(context)
        {
        }
        public override void Update(GameTime gameTime)
        {
            return;
        }
        public override void Draw()
        {
            SpriteBatch sb = context.SpriteBatch;
            var viewport = context.Graphics.GraphicsDevice.Viewport;
            sb.Begin(samplerState: SamplerState.PointClamp);
            sb.Draw(context.Assets.Sprites.Empty.Texture, 
                new Rectangle(0, 0, viewport.Width, viewport.Width), 
                context.Assets.Sprites.Empty.SourceRect, Color.White );
          
            sb.DrawStringCentered("You died..", new Vector2(viewport.Width/2, 20), 8, Color.White);
            
            sb.DrawStringCentered("Score: " + context.Score.Value, new Vector2(viewport.Width/2, 100), 3, Color.White);
            
            /*sb.Draw(context.Assets.Sprites.Paper.Texture, 
                new Rectangle(0, 300, viewport.Width, viewport.Width), 
                context.Assets.Sprites.Empty.SourceRect, Color.White );*/
            
            sb.DrawStringCentered("Play again", new Vector2(viewport.Width/2, 300), 6, Color.White);
            sb.End();
        }
    }
}