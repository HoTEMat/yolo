using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo
{
    public class PersonBehavior : Interactable
    {
        private const double interactionSuccess = 0.5;
        private List<Vector2> TargetPoints;
        private Vector2 curTargetPoint;
        private const float walkSpeed = 1;
        private SpriteOrientationManager orientationManager;
        
        public PersonBehavior(int spriteNum, List<Vector2> targetPoints)
        {
            TargetPoints = targetPoints;
            curTargetPoint = Utils.RandChoice(TargetPoints);
            orientationManager = new SpriteOrientationManager(spriteNum, Entity);
        }
        public override void Update()
        {
            var distanceFromTarget = (Position - curTargetPoint).Length();
            if (distanceFromTarget <= 1) // ToDo: What is close distance?
            {
                changeTarget();
            }
            var direction = curTargetPoint - Position;
            direction.Normalize();
            var delta = direction * walkSpeed;
            Position += delta;
            orientationManager.UpdateOrientation(delta);
        }
        private void changeTarget()
        {
            curTargetPoint = Utils.RandChoice(TargetPoints);
        }
        public override AchievementType? Interact()
        {
            // curse or hug
            var rnd = Entity.Context.Random.NextDouble();
            if (rnd <= interactionSuccess) // success
            {
                Entity.Destroy();
                return Entity.Context.Player.IsGood ? AchievementType.HugPerson : AchievementType.CursePerson;
            }
            return null;
        }
        public override bool CanInteract()
        {
            return true;
        }
    }
}