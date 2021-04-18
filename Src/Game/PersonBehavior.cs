using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo
{
    public class PersonBehavior : Interactable
    {
        private const double interactionSuccess = 1;
        private List<Vector3> TargetPoints;
        private Vector3 curTargetPoint;
        private const float walkSpeed = 1f;
        private SpriteOrientationManager orientationManager;
        
        private bool interacted;
        private int point = 0;
        
        private bool walking;
        private float activityTime;
        private float nextStopTime = (float)Utils.Random.NextDouble();
        
        public PersonBehavior(int spriteNum, List<Vector3> targetPoints, Entity entity) : base(entity)
        {
            TargetPoints = targetPoints;
            orientationManager = new SpriteOrientationManager(spriteNum, Entity);

            point = Utils.Random.Next(0, targetPoints.Count);
            Position = targetPoints[point];
            curTargetPoint = targetPoints[point];
        }
        
        public override void Update()
        {
            var distanceFromTarget = (Position - curTargetPoint).Length();
            if (distanceFromTarget <= 0.01)
            {
                nextState();
            }

            float dt = (float) Context.GameTime.ElapsedGameTime.TotalSeconds;

            if (nextStopTime < 0.1 * activityTime * (walking ? 1 : 0.1))
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
            else
                orientationManager.UpdateOrientation(Vector3.Zero);
            
            activityTime += dt;
        }
        private void nextState()
        {
            if (point == 0)
                point += 1;
            else if (point == TargetPoints.Count - 1)
                point -= 1;
            else
                point = point += Utils.Random.Next(-1, 2);
                    
            curTargetPoint = new Vector3(
                TargetPoints[point].X + (float)Utils.Random.NextDouble(),
                TargetPoints[point].Y + (float)Utils.Random.NextDouble(),
                TargetPoints[point].Z);
        }
        public override AchievementType? Interact()
        {
            // curse or hug
            var rnd = Entity.Context.Random.NextDouble();
            interacted = true;
            
            if (rnd <= interactionSuccess) // success
                return Entity.Context.Player.IsGood ? AchievementType.HugPerson : AchievementType.CursePerson;
            
            return null;
        }
        public override bool CanInteract()
        {
            return !interacted;
        }
    }
}