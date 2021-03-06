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
        Vector3 PushFrom(Entity other);

        float DistanceFromBorderTo(Vector3 point);
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

        public abstract Vector3 PushFrom(Entity other);

        public abstract float DistanceFromBorderTo(Vector3 point);
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
        public override Vector3 PushFrom(Entity other)
        {
            Vector2 other_pos = new Vector2(other.Position.X, other.Position.Y);
            Vector2 my_pos = new Vector2(ColliderEntity.Position.X, ColliderEntity.Position.Y);

            switch (other.Collider)
            {
                // CIRCLE from CIRCLE
                case CircleCollider otherCircleCollider:
                {
                    float distance = (float)Math.Sqrt(Math.Pow(other_pos.X - my_pos.X, 2) + Math.Pow(other_pos.Y - my_pos.Y, 2));

                    if (distance < otherCircleCollider.Radius + Radius)
                    {
                        Vector2 moveVector = new Vector2(my_pos.X - other_pos.X, my_pos.Y - other_pos.Y);
                        moveVector.Normalize();
                        return new Vector3(moveVector * (otherCircleCollider.Radius + Radius - distance), 0);
                    }

                    break;
                }
                
                // CIRCLE from RECTANGLE
                case RectangleCollider otherRectangleCollider:
                {
                    // the other object is at 0, 0, I'm in the 1st quadrant
                    Vector2 pos = (my_pos - other_pos);
                    float sx = Math.Sign(pos.X);
                    float sy = Math.Sign(pos.Y);
                    pos = new Vector2(Math.Abs(pos.X), Math.Abs(pos.Y));

                    float w = otherRectangleCollider.Width / 2f;
                    float h = otherRectangleCollider.Height / 2f;
                    
                    // we're above
                    if (pos.X <= w && pos.Y - Radius <= h)
                        return new Vector3(0, (h - (pos.Y - Radius)) * sy, 0);
                
                    // we're to the right
                    if (pos.Y <= h && pos.X - Radius <= w)
                        return new Vector3((w - (pos.X - Radius)) * sx, 0, 0);

                    // we're at the corner (or inside, but we're fucked in that case)
                    Vector2 corner = new Vector2(w, h);

                    if ((corner - pos).Length() < Radius)
                    {
                        Vector2 result = -(corner - pos);
                        result.Normalize();
                        result *= Radius - (corner - pos).Length();
                        return new Vector3(result.X * sx, result.Y * sy, 0);
                    }
                    
                    break;
                }
            }

            return Vector3.Zero;
        }

        public override float DistanceFromBorderTo(Vector3 point) {
            return Math.Abs((point - ColliderEntity.Position).Length() - Radius);
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
        public override Vector3 PushFrom(Entity other)
        {
            return Vector3.Zero;
        }

        public override float DistanceFromBorderTo(Vector3 point) {
            Vector3 ourCenter = ColliderEntity.Position;
            Vector3 diag = new Vector3(-Width, Height, 0);
            Vector3 topLeft = ourCenter + diag / 2;
            Vector3 botRight = ourCenter - diag / 2;

            float dx = Utils.Max(0, topLeft.X - point.X, point.X - botRight.X);
            float dy = Utils.Max(0, topLeft.Y - point.Y, point.Y - botRight.Y);
            return (float) Math.Sqrt(dx * dx + dy * dy);
        }
    }
}