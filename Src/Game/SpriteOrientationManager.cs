using Microsoft.Xna.Framework;

namespace yolo {
    public class SpriteOrientationManager {
        private int spriteNum;
        private Entity entity;
        private PersonOrientation Orientation { get; set; } = PersonOrientation.Down;
        private bool Walking { get; set; } = false;

        public SpriteOrientationManager(int spriteNum, Entity entity) {
            this.spriteNum = spriteNum;
            this.entity = entity;
            
            entity.ChangeSpriteTo(PersonSpriteSelector.GetStaticSprite(entity.Context.Assets, spriteNum, PersonOrientation.Down));
        }
        
        public void UpdateOrientation(Vector2 posChange) {
            Context ctx = entity.Context;
            PersonOrientation? newOrientation = GetOrientation(posChange);
            bool newWalking = newOrientation != null;
            if (newWalking && (!Walking || newOrientation.Value != Orientation)) {
                TimedSpriteSet newSprite = PersonSpriteSelector.GetWalkingSpriteSet(ctx.Assets, spriteNum, newOrientation.Value);
                Orientation = (PersonOrientation) newOrientation;
                entity.ChangeSpriteTo(newSprite);
            } else if (!newWalking && Walking) {
                Sprite newSprite = PersonSpriteSelector.GetStaticSprite(ctx.Assets, spriteNum, Orientation);
                entity.ChangeSpriteTo(newSprite);
            }
            Walking = newWalking;
        }

        private static PersonOrientation? GetOrientation(Vector2 posChange) {
            float epsilon = 10e-3f;
            if (posChange.Y > epsilon)
                return PersonOrientation.Down;
            if (posChange.Y < -epsilon)
                return PersonOrientation.Up;
            if (posChange.X > epsilon)
                return PersonOrientation.Right;
            if (posChange.X < -epsilon)
                return PersonOrientation.Left;
            return null;
        }
    }
    
    public enum PersonOrientation {
        Up,
        Right,
        Down,
        Left
    }
}

