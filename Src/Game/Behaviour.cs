namespace yolo {
    public abstract class Behaviour {
        public Entity Entity { get; }
        public abstract void Update();
    }

    public abstract class Interactable : Behaviour {
        public abstract void Interact();

        public bool Highlighted { get; private set; }
        public void SetHighlighted(bool highlighted) {
            if (highlighted == Highlighted)
                return;
            if (highlighted) {
                Entity.ChangeSpriteTo(HighlightedSprite);
            } else {
                Entity.ChangeSpriteTo(DefaultSprite);
            }

            Highlighted = highlighted;
        }

        protected abstract TimedSpriteSet DefaultSprite { get; }
        protected abstract TimedSpriteSet HighlightedSprite { get; }
    }
}