using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yolo {
    static class SpritebatchExtension {
        public static void DrawSprite(this SpriteBatch sb, Sprite sprite, Vector2 position) {
            sb.Draw(sprite.Texture, position, sprite.SourceRect, sprite.Tone, 0f, sprite.Origin, 1f, sprite.Effects, 0f);
        }
        public static void DrawSprite(this SpriteBatch sb, Sprite sprite, Vector2 position, float scale) {
            sb.Draw(sprite.Texture, position, sprite.SourceRect, sprite.Tone, 0f, sprite.Origin, scale, sprite.Effects, 0f);
        }
        public static void DrawSprite(this SpriteBatch sb, Sprite sprite, Vector3 position) {
            sb.Draw(sprite.Texture, new Vector2(position.X, position.Y), sprite.SourceRect, sprite.Tone, 0f, sprite.Origin, 1 / 16f, sprite.Effects, position.Z);
        }

        public static void DrawTextBlock(this SpriteBatch sb, SpriteFont font, Vector2 position, string text,
            int charsPerRow, int lineHeight, int scale, Color fontColor)
        {
            Vector2 curPosition = position;
            string[] words = text.Split();
            string curLine = "";
            int wordIdx = 0;
            while (wordIdx < words.Length)
            {
                while (curLine.Length + words[wordIdx].Length < charsPerRow)
                {
                    curLine += words[wordIdx];
                    curLine += " ";
                    wordIdx++;
                    if (wordIdx >= words.Length) break;
                }
                sb.DrawString(font, curLine, curPosition, fontColor, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                curLine = "";
                curPosition = new Vector2(curPosition.X, curPosition.Y + lineHeight);
            }
        }
    }
}
