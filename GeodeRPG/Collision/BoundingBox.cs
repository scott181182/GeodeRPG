using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Realm;
using static SuperMarioYeah.Collision.Direction;

namespace SuperMarioYeah.Collision
{
    public class BoundingBox : IPolygon
    {
        public const float EPSILON = 0.1f;

        public float Height { get { return (float)box.Height / World.BLOCK_SIZE; } }
        public float Width { get { return (float)box.Width / World.BLOCK_SIZE; } }

        private Rectangle box;
        public Rectangle  Box { get { return this.box; } }
        
        /** Makes a non-relative Camera coordinate BoundingBox from World Coordinates */
        public BoundingBox(float x, float y, float width, float height)
        {
            box = new Rectangle(
                (int)(x * World.BLOCK_SIZE),
                (int)(((float)World.WORLD_HEIGHT - y - height) * World.BLOCK_SIZE),
                (int)(width * World.BLOCK_SIZE),
                (int)(height * World.BLOCK_SIZE));
        }

        public bool Visible { get; set; }

        public Direction DoesIntersect(IPolygon p)
        {
            Direction dir = Direction.None;

            if (p is BoundingBox)
            {
                Rectangle neighborBox = (p as BoundingBox).box;

                if (box.Intersects(neighborBox))
                {
                    float dx = Math.Min(box.Right, neighborBox.Right) - Math.Max(box.Left, neighborBox.Left);
                    float dy = Math.Min(box.Bottom, neighborBox.Bottom) - Math.Max(box.Top, neighborBox.Top);

                    if (dx < dy)
                    {
                        dir = box.Left - neighborBox.Left < 0 ? Direction.Right : Direction.Left;
                    }
                    else
                    {
                        dir = box.Top - neighborBox.Top > 0 ? Direction.Up : Direction.Down;
                    }
                }
            }

            return dir;
        }

        public Vector2 Overlap(IPolygon p)
        {
            if(p is BoundingBox)
            {
                Rectangle other = (p as BoundingBox).box;
                if (box.Intersects(other))
                {
                    Rectangle intersect = Rectangle.Intersect(box, other);
                    return new Vector2(
                        (float)intersect.Width / World.BLOCK_SIZE,
                        (float)intersect.Height / World.BLOCK_SIZE);
                }
            }
            return Vector2.Zero;
        }
    }
}
