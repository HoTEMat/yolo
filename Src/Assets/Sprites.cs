using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo
{
    public interface ISpriteSet
    {
        public bool Loops { get; }
        public Sprite GetSpriteAt(int millis);
    }


    public class TimedSpriteSet : ISpriteSet
    {
        public Sprite[] Sprites { get; init; }
        public bool Loops { get; init; }
        public int Period { get; init; }

        public Sprite GetSpriteAt(int millis = 500)
        {
            int frame = (millis / Period) % Sprites.Length;
            return Sprites[frame];
        }
    }

    public class Sprite : ISpriteSet
    {
        public Texture2D Texture { get; init; }
        public SpriteEffects Effects { get; init; } = SpriteEffects.None;
        public Rectangle SourceRect { get; init; }
        public Vector2 Origin { get; init; } = Vector2.Zero;
        public Color Tone { get; init; } = Color.White;

        public bool Loops { get; } = false;
        public Sprite GetSpriteAt(int millis) => this;

        public Vector2 UVTopLeft => new Vector2(SourceRect.X, SourceRect.Y) / new Vector2(Texture.Width, Texture.Height);
        public Vector2 UVTopRight => new Vector2(SourceRect.X + SourceRect.Width, SourceRect.Y) / new Vector2(Texture.Width, Texture.Height);
        public Vector2 UVBotLeft => new Vector2(SourceRect.X, SourceRect.Y + SourceRect.Height) / new Vector2(Texture.Width, Texture.Height);
        public Vector2 UVBotRight => new Vector2(SourceRect.X + SourceRect.Width, SourceRect.Y + SourceRect.Height) / new Vector2(Texture.Width, Texture.Height);
    }
}