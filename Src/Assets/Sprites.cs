using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class TimedSpriteSet {
        public bool IsFlat { get; }
        public Sprite GetSpriteAt(int millis) {
            throw new NotImplementedException();
        }
    }

    public class Sprite {
        public Texture2D Texture { get; init; }
        public Rectangle SourceRect { get; init; }
        public Vector2 Origin { get; init; } = Vector2.Zero;
        public Color Tone { get; init; } = Color.White;
    }
}