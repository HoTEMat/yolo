using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public static class DrawUtils {
        public static SpriteFont Font { get; set; }
        
        public static void DrawString(this SpriteBatch spriteBatch, string s, Vector2 position, float scale, Color fontColor) {
            spriteBatch.DrawString(Font, s, position, fontColor, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public static void DrawStringCentered(this SpriteBatch spriteBatch, string s, Vector2 center, float scale, Color fontColor) {
            float charW = Font.Glyphs[0].Width;
            float strW = s.Length * charW;
            float strH = charW;
            
            DrawString(spriteBatch, s, center - new Vector2(strW / 2, strH / 2) * scale, scale, fontColor);
        }
    }
}