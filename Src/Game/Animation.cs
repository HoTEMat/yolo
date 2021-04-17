using System;

namespace yolo {
    public class Animation {
        private float millis;
        private ISpriteSet sprites;

        public Animation(TimedSpriteSet sprites) {
            this.sprites = sprites;
            millis = 0;
        }
        
        public void Update(Context ctx) {
            millis += (float) ctx.GameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public Sprite GetCurrentSprite() {
            return sprites.GetSpriteAt((int) millis);
        }
    }
}