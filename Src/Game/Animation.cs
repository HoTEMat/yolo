using System;

namespace yolo {

    public interface IAnimation {
        public void Update(Context ctx);
        public Sprite GetCurrentSprite();
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

        public Sprite GetCurrentSprite() {
            return sprites.GetSpriteAt((int)millis);
        }
    }

    public class DialogAnimation : IAnimation {
        public Sprite GetCurrentSprite() {
            throw new NotImplementedException();
        }

        public void Update(Context ctx) {
            throw new NotImplementedException();
        }
    }
}