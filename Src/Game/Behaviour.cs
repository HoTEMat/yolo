using Microsoft.Xna.Framework;

namespace yolo {
    public abstract class Behaviour {
        public Entity Entity { get; }
        public Vector2 Position {
            get => Entity.Position;
            set => Entity.Position = value;
        }

        public Context Context => Entity.Context;
        public abstract void Update();
    }

    public abstract class Interactable : Behaviour {
        public abstract AchievementType? Interact();

        public abstract bool CanInteract();
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

    public class Bin : Interactable
    {
        public bool IsOverturned { get; private set; }
        public override void Update()
        {
            return;
        }

        public override AchievementType? Interact()
        {
            if (IsOverturned)
            {
                IsOverturned = false;
                return AchievementType.PutUpBin;
            }
            IsOverturned = true;
            return AchievementType.ToppleBin;
        }

        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood == IsOverturned;
        }

        protected override TimedSpriteSet DefaultSprite { get; }
        protected override TimedSpriteSet HighlightedSprite { get; }
    }
}