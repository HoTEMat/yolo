using System;
using Microsoft.Xna.Framework;

namespace yolo {
    public interface ICollider {
        /// <summary>
        /// The entity of this collider.
        /// </summary>
        Entity ColliderEntity { get; }
        
        /// <summary>
        /// Whether it is movable or not.
        /// </summary>
        bool Movable { get; }
        
        /// <summary>
        /// Return vector to push this entity away from the other.
        /// </summary>
        /// <param name="other">The entity to push from.</param>
        Vector2 PushFrom(Entity other);
    }

    abstract class Collider : ICollider
    {
        protected Collider(Entity ColliderEntity, bool Movable)
        {
            this.ColliderEntity = ColliderEntity;
            this.Movable = Movable;
        }

        public Entity ColliderEntity { get; }
        public bool Movable { get; }

        public abstract Vector2 PushFrom(Entity other);
    }

    class CircleCollider : Collider
    {
        public readonly float Radius;
        
        public CircleCollider(Entity colliderEntity, bool movable, float radius) : base(colliderEntity, movable)
        {
            this.Radius = radius;
        }
        
        /// <summary>
        /// Pushes this entity away from the other as to not collide with it.
        /// </summary>
        /// <param name="other"></param>
        public override Vector2 PushFrom(Entity other)
        {
            switch (other.Collider)
            {
                // CIRCLE from CIRCLE
                case CircleCollider otherCircleCollider:
                {
                    Vector2 other_pos = other.Position;
                    Vector2 my_pos = ColliderEntity.Position;

                    float distance = (float)Math.Sqrt(Math.Pow(other_pos.X - my_pos.X, 2) + Math.Pow(other_pos.Y - my_pos.Y, 2));

                    if (distance < otherCircleCollider.Radius + Radius)
                    {
                        Vector2 moveVector = new Vector2(my_pos.X - other_pos.X, my_pos.Y - other_pos.Y);
                        moveVector.Normalize();
                        return moveVector * (otherCircleCollider.Radius + Radius - distance);
                    }

                    break;
                }
                
                // CIRCLE from RECTANGLE
                case RectangleCollider otherRectangleCollider:
                {
                    // the other object is at 0, 0, I'm in the 1st quadrant
                    Vector2 pos = (ColliderEntity.Position - other.Position);
                    float sx = Math.Sign(pos.X);
                    float sy = Math.Sign(pos.Y);
                    pos = new Vector2(Math.Abs(pos.X), Math.Abs(pos.Y));
                
                    // we're above
                    if (pos.X < otherRectangleCollider.Width && pos.X - Radius < otherRectangleCollider.Height)
                        return new Vector2(0, -otherRectangleCollider.Height - (pos.X - Radius) * sy);
                
                    // we're to the right
                    if (pos.Y < otherRectangleCollider.Height && pos.Y - Radius < otherRectangleCollider.Width)
                        return new Vector2(otherRectangleCollider.Width - (pos.Y - Radius) * sx, 0);
                
                    // we're at the corner (or inside, but we're fucked in that case)
                    // TODO!
                    break;
                }
            }

            return Vector2.Zero;
        }
    }
    
    class RectangleCollider : Collider
    {
        public float Width;
        public float Height;
        
        public RectangleCollider(Entity colliderEntity, bool movable, float width, float height) : base(colliderEntity, movable)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Pushes this entity away from the other as to not collide with it.
        /// </summary>
        /// <param name="other"></param>
        public override Vector2 PushFrom(Entity other)
        {
            return Vector2.Zero;
        }
    }
    
}