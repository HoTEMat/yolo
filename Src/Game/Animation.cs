using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace yolo {

    public interface IAnimation : IDisposable {
        void Update(Context ctx);
        Sprite GetCurrentSprite(Context ctx);
        void Reset(ISpriteSet spriteset);
        bool Highlighted { get; set; }
        public float Scale { get; set; }
        bool IsFlat { get; set; }
    }

    public class Animation : IAnimation {
        private float millis;
        private ISpriteSet sprites;
        public bool Highlighted { get; set; } = false;
        public bool IsFlat { get; set; }
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float Scale { get; set; } = 1;

        public Animation(ISpriteSet sprites) {
            this.sprites = sprites;
            millis = 0;
        }
        public void Reset(ISpriteSet newSprites) {
            sprites = newSprites;
            millis = 0;
        }
        public void Update(Context ctx) {
            millis += (float)ctx.GameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public Sprite GetCurrentSprite(Context ctx) {
            var result = sprites.GetSpriteAt((int)millis).Clone();
            result.Effects ^= Effects;
            result.Scale *= this.Scale;

            return result;
        }

        public void Dispose() { }
    }

    public class DialogAnimation : IAnimation {

        float charsToPrint;
        public string Text { get; set; }
        public float CharsPerSec { get; set; } = 10f;
        public Point DisplayChars { get; init; } = new(18, 3);
        public bool Highlighted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Scale { get; set; } = 1f;
        public bool IsFlat { get => true; set => throw new NotImplementedException(); }

        RenderTarget2D buffer;
        void initBuffer(Context ctx) {
            buffer = new RenderTarget2D(ctx.Graphics.GraphicsDevice, DisplayChars.X * 7, DisplayChars.Y * 7);
        }

        public Sprite GetCurrentSprite(Context ctx) {
            if (buffer == null) {
                initBuffer(ctx);
            }

            Sprite sprite = new() {
                Origin = new(buffer.Width / 2, buffer.Height),
                SourceRect = buffer.Bounds,
                Texture = buffer,
            };

            var font = ctx.Assets.Fonts.Font;

            var sb = ctx.SpriteBatch;
            var device = ctx.Graphics.GraphicsDevice;
            device.SetRenderTarget(buffer);
            sb.Begin(samplerState: SamplerState.PointClamp);
            {
                sb.FillRectangle(buffer.Bounds, Color.White);
                sb.DrawTextBlock(font, new(1, 1), Text.Substring(0, (int)charsToPrint), DisplayChars.X, 7, 1, Color.Black);
            }
            sb.End();
            device.SetRenderTarget(null);

            return sprite;
        }

        public void Update(Context ctx) {
            charsToPrint += ctx.dSec * CharsPerSec;
        }

        public void Dispose() {
            buffer.Dispose();
        }

        public void Reset(ISpriteSet spriteset) {
            throw new NotImplementedException();
        }
    }
}