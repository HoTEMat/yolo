using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class HUD {
        private Context context;
        private AssetBank assets => context.Assets;

        public HUD(Context context) {
            this.context = context;
        }

        public void Draw() {
            var device = context.Graphics.GraphicsDevice;
            var camera = context.Camera;
            var viewport = context.Graphics.GraphicsDevice.Viewport;
            
            context.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            DrawClock(context.SpriteBatch, new Vector2(150, 90), GetClockContent());
            context.SpriteBatch.End();
        }

        private string GetClockContent() {
            World w = context.World;
            if (w == null)
                return "---";
            int ttl = (int) w.TimeToLive;
            int minutes = ttl % 60;
            int seconds = ttl / 60;
            return minutes + ":" + seconds;
        }

        private void DrawClock(SpriteBatch spriteBatch, Vector2 center, string content) {
            const int clockWidth = 250;
            const int clockHeight = 150;
            Vector2 toUpperLeft = new Vector2(-clockWidth / 2, -clockHeight / 2);
            spriteBatch.Draw(
                assets.Sprites.Empty.Texture,
                new Rectangle((int)(center.X + toUpperLeft.X), (int)(center.Y + toUpperLeft.Y), clockWidth, clockHeight),
                assets.Sprites.Empty.SourceRect,
                Color.White);
            DrawStringCentered(spriteBatch, content, center, 8);
        }

        private void DrawString(SpriteBatch spriteBatch, string s, Vector2 position, int scale) {
            spriteBatch.DrawString(assets.Fonts.Font, s, position, Color.Red, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        private void DrawStringCentered(SpriteBatch spriteBatch, string s, Vector2 center, int scale) {
            float charW = assets.Fonts.Font.Glyphs[0].Width;
            float strW = s.Length * charW;
            float strH = charW;
            
            DrawString(spriteBatch, s, center - new Vector2(strW / 2, strH / 2) * scale, scale);
        }
    }
}