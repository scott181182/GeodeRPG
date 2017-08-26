using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Realm;

namespace SuperMarioYeah.Collision
{
    public class EntityCollisionInstance : ICollisionInstance
    {
        public ICollidable c1, c2;
        public Direction dir;
        public World world;


        public EntityCollisionInstance(ICollidable c1, ICollidable c2, Direction dir, World world)
        {
            this.c1 = c1;
            this.c2 = c2;
            this.dir = dir;
            this.world = world;
        }

        public void DoCollision()
        {
            c1.OnCollision(c2, dir, world);
            c2.OnCollision(c1, dir.Reverse(), world);
        }

        public Vector2 GetOverlap() { return c1.BoundingPolygon.Overlap(c2.BoundingPolygon); }
        public float GetArea()
        {
            Vector2 overlap = GetOverlap();
            return overlap.X * overlap.Y; 
        }

        public int CompareTo(ICollisionInstance other)
        {
            return other.GetArea().CompareTo(GetArea());
        }
        public bool Equals(ICollisionInstance other)
        {
            EntityCollisionInstance o = other as EntityCollisionInstance;
            return o != null
                && GetArea() == o.GetArea()
                && c1 == o.c1
                && c2 == o.c2;
        }
    }
}
