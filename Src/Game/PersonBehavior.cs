using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo
{
    public class PersonBehavior : Interactable
    {
        private const double interactionSuccess = 0.5;
        private List<Vector3> TargetPoints;
        private Vector3 curTargetPoint;
        private const float walkSpeed = 1f;
        private SpriteOrientationManager orientationManager;
        
        private bool interacted;
        
        private bool walking;
        private float activityTime;
        private float nextStopTime = (float)Utils.Random.NextDouble();
        
        public PersonBehavior(int spriteNum, List<Vector3> targetPoints, Entity entity) : base(entity)
        {
            TargetPoints = targetPoints;
            orientationManager = new SpriteOrientationManager(spriteNum, Entity);
            nextState();
        }
        
        public override void Update()
        {
            var distanceFromTarget = (Position - curTargetPoint).Length();
            if (distanceFromTarget <= 0.01)
            {
                nextState();
            }

            float dt = (float) Context.GameTime.ElapsedGameTime.TotalSeconds;

            if (nextStopTime < 0.1 * activityTime * (walking ? 1 : 0.05))
            {
                walking = !walking;
                nextStopTime = (float)Utils.Random.NextDouble();
                activityTime = 0;
            }

            if (!walking)
            {
                var direction = curTargetPoint - Position;
                direction.Normalize();
                var delta = direction * walkSpeed * dt;
                Position += delta;
                orientationManager.UpdateOrientation(delta);
            }
            
            activityTime += dt;
        }
        private void nextState()
        {
            curTargetPoint = Utils.RandChoice(TargetPoints);
        }
        public override AchievementType? Interact()
        {
            // curse or hug
            var rnd = Entity.Context.Random.NextDouble();
            if (rnd <= interactionSuccess) // success
            {
                interacted = true;
                return Entity.Context.Player.IsGood ? AchievementType.HugPerson : AchievementType.CursePerson;
            }
            return null;
        }
        public override bool CanInteract()
        {
            return !interacted;
        }
    }
}