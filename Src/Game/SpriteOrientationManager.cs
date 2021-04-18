using System;
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
        
        public void UpdateOrientation(Vector3 posChange) {
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

        private static PersonOrientation? GetOrientation(Vector3 posChange)
        {
            double res = Math.Atan2(posChange.Y, posChange.X);

            if (posChange.Length() < 10e-7f)
                return null;
            
            if (res <= Math.PI / 4 && res >= -Math.PI/4)
                return PersonOrientation.Right;
            if (res < Math.PI * 3 / 4 && res > Math.PI/4)
                return PersonOrientation.Down;
            if (res <= -Math.PI / 4 && res > -Math.PI* 3/4)
                return PersonOrientation.Up;
            return PersonOrientation.Left;
        }
    }
    
    public enum PersonOrientation {
        Up,
        Right,
        Down,
        Left
    }
}

