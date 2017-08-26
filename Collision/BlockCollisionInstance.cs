using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Realm;

namespace SuperMarioYeah.Collision
{
    public class BlockCollisionInstance : ICollisionInstance
    {
        public IBlockCollidable block;
        public ICollidable entity;
        public int x, y;
        public Direction dir;
        public World world;


        public BlockCollisionInstance(int x, int y, IBlockCollidable c1, ICollidable c2, Direction dir, World world)
        {
            this.x = x;
            this.y = y;
            this.block = c1;
            this.entity = c2;
            this.dir = dir;
            this.world = world;
        }

        public void DoCollision()
        {
            block.OnCollision(x, y, entity, dir.Reverse(), world);
            entity.OnCollision(block, dir, world);
        }

        public Vector2 GetOverlap() {
            return entity.BoundingPolygon.Overlap(new BoundingBox(x, y, 1, 1));
        }
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
            BlockCollisionInstance o = other as BlockCollisionInstance;
            return o != null
                && GetArea() == o.GetArea()
                && entity == o.entity
                && block == o.block
                && x == o.x
                && y == o.y;
        }
    }
}
