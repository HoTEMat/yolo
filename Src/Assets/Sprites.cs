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
        public Rectangle Bounds { get; init; }
        public Point Origin { get; init; }
        public Color Tone { get; init; }
    }
}