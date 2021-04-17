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
        
        public PersonBehavior(List<Vector2> targetPoints)
        {
            TargetPoints = targetPoints;
        }

        public override void Update()
        {
            var distanceFromTarget = (Position - curTargetPoint).Length();
            if (distanceFromTarget <= 1) // ToDo: What is close distance?
            {
                changeTarget();
            }
            var direction = curTargetPoint - Position;
            var delta = (direction / direction.Length()) * walkSpeed;
            Position += delta;
        }
        private void changeTarget()
        {
            curTargetPoint = Utils.RandChoice(TargetPoints);
        }
        public override void Interact()
        {
            // infect or hug
            var rnd = Entity.Context.Random.NextDouble();
            if (rnd <= interactionSuccess) // success
            {
                
            }
            else // fail
            {
                
            }
            
        }

        protected override TimedSpriteSet DefaultSprite { get; }
        protected override TimedSpriteSet HighlightedSprite { get; }
    }
}