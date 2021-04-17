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
        public Texture2D Texture;
        public Rectangle Bounds;
        public Point Origin;
        public Color Tone;
    }
}