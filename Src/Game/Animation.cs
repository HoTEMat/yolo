using Microsoft.Xna.Framework.Graphics;
using System;

namespace yolo {

    public interface IAnimation : IDisposable {
        public void Update(Context ctx);
        public Sprite GetCurrentSprite(Context ctx);
    }

    public class Animation : IAnimation {
        private float millis;
        private ISpriteSet sprites;
        public bool Highlighted { get; set; } = false;

        public Animation(ISpriteSet sprites) {
            this.sprites = sprites;
            millis = 0;
        }
        public void Reset(ISpriteSet newSprites)
        {
            sprites = newSprites;
            millis = 0;
        }
        public void Update(Context ctx) {
            millis += (float) ctx.GameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public Sprite GetCurrentSprite(Context ctx) {
            return sprites.GetSpriteAt((int)millis);
        }

        public void Dispose() {}
    }

    public class DialogAnimation : IAnimation {

        int characters;
        string actualText;
        public string Text { get; set; }
        public double MillisPerLetter { get; set; }

        RenderTarget2D buffer;
        void initBuffer(Context ctx) {
            buffer = new RenderTarget2D(ctx.Graphics.GraphicsDevice, 32, 20);
        }

        public Sprite GetCurrentSprite(Context ctx) {
            if (buffer == null) {
                initBuffer(ctx);
            }

            throw new NotImplementedException();
        }

        public void Update(Context ctx) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            buffer.Dispose();
        }
    }
}