using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo
{
    public interface ISpriteSet
    {
        public bool IsFlat { get; }
        public bool Loops { get; }
        public Sprite GetSpriteAt(int millis);
    }

    public class SimpleSpriteSet : ISpriteSet
    {
        private Sprite Sprite { get; init; }
        public bool IsFlat { get; init; }
        public bool Loops { get; } = false;

        public Sprite GetSpriteAt(int millis) => Sprite;
    }

    public class TimedSpriteSet : ISpriteSet
    {
        public Sprite[] Sprites { get; init; }
        public bool IsFlat { get; init; }
        public bool Loops { get; init; }
        public int Period { get; init; }
        
        public Sprite GetSpriteAt(int millis = 500)
        {
            int frame = (millis / Period) % Sprites.Length;
            return Sprites[frame];
        }
    }

    public class Sprite
    {
        public Texture2D Texture { get; init; }
        public SpriteEffects Effects { get; init; } = SpriteEffects.None;
        public Rectangle SourceRect { get; init; }
        public Vector2 Origin { get; init; } = Vector2.Zero;
        public Color Tone { get; init; } = Color.White;
    }
}