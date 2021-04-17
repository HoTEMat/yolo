using System;

namespace yolo {
    public class Animation {
        private int millis;
        private TimedSpriteSet sprites;

        public Animation(TimedSpriteSet sprites) {
            this.sprites = sprites;
            millis = 0;
        }
        
        public void Update(Context ctx) {
            throw new NotImplementedException();
        }

        public Sprite GetCurrentSprite() {
            throw new NotImplementedException();
        }
    }
}