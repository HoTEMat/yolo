using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public interface ISpriteSet {
        public bool Loops { get; }
        public Sprite GetSpriteAt(int millis);
    }


    public class TimedSpriteSet : ISpriteSet {
        public Sprite[] Sprites { get; init; }
        public bool Loops { get; init; }
        public int Period { get; init; }

        public Sprite GetSpriteAt(int millis = 500) {
            int frame = (millis / Period) % Sprites.Length;
            return Sprites[frame];
        }
    }

    public class Sprite : ISpriteSet {

        public Texture2D Texture { get; set; }
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public Rectangle SourceRect {
            get => sourceRect;
            set {
                sourceRect = value;
                if (!originSet) {
                    origin = value.Size.ToVector2() / 2f;
                }
            }
        }
        private Rectangle sourceRect;
        bool originSet = false;

        public Vector2 Origin {
            get => origin;
            set {
                origin = value;
                originSet = true;
            }
        }

        private Vector2 origin = Vector2.Zero;
        public Color Tone { get; set; } = Color.White;
        public bool Loops => false;
        public float Scale { get; set; } = 1f;
        public Sprite GetSpriteAt(int millis) => this;

        public Sprite Clone() {
            return new Sprite() {
                Texture = this.Texture,
                Effects = this.Effects,
                SourceRect = this.SourceRect,
                Origin = this.Origin,
                Tone = this.Tone,
                Scale = this.Scale
            };
        }

        public Vector2 UVTopLeft => new Vector2(SourceRect.X, SourceRect.Y) / new Vector2(Texture.Width, Texture.Height);
        public Vector2 UVTopRight => new Vector2(SourceRect.X + SourceRect.Width, SourceRect.Y) / new Vector2(Texture.Width, Texture.Height);
        public Vector2 UVBotLeft => new Vector2(SourceRect.X, SourceRect.Y + SourceRect.Height) / new Vector2(Texture.Width, Texture.Height);
        public Vector2 UVBotRight => new Vector2(SourceRect.X + SourceRect.Width, SourceRect.Y + SourceRect.Height) / new Vector2(Texture.Width, Texture.Height);
    }
}