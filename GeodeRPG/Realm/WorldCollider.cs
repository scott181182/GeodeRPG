using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Realm
{
    public class WorldCollider
    {
        private World world;
        private Camera camera;

        public WorldCollider(World world, Camera camera)
        {
            this.world = world;
            this.camera = camera;
        }

        public void HandleCollisions(List<IEntity> entities, int[,] tiles)
        {
            DoEntityCollisions(entities);
            DoBlockCollisions(entities, tiles);
        }

        private void DoEntityCollisions(List<IEntity> entities)
        {
            List<ICollidable> collidables = new List<ICollidable>(entities.OfType<ICollidable>());
            List<ICollisionInstance> doneCollisions = new List<ICollisionInstance>();

            while (true)
            {
                List<ICollisionInstance> collisionQueue = CalcEntityCollisions(collidables);
                if (collisionQueue.Count == 0) { return; }
                if (!DoNextCollision(collisionQueue, doneCollisions)) { break; }
            }
        }
        private void DoBlockCollisions(List<IEntity> entities, int[,] tiles)
        {
            List<IEntityCollidable> collidables = new List<IEntityCollidable>(entities.OfType<IEntityCollidable>());
            List<ICollisionInstance> doneCollisions = new List<ICollisionInstance>();

            while (true)
            {
                List<ICollisionInstance> collisionQueue = CalcBlockCollisions(collidables, tiles);
                if (collisionQueue.Count == 0) { break; }
                if(!DoNextCollision(collisionQueue, doneCollisions)) { break; }
            }
        }



        private List<ICollisionInstance> CalcEntityCollisions(List<ICollidable> entities)
        {
            List<ICollisionInstance> collisionQueue = new List<ICollisionInstance>();

            for (int i = 0; i < entities.Count - 1; i++)
            {
                for (int j = i + 1; j < entities.Count; j++)
                {
                    Direction dir = entities[i].BoundingPolygon.DoesIntersect(entities[j].BoundingPolygon);
                    if (dir != Direction.None)
                    {
                        collisionQueue.Add(new EntityCollisionInstance(entities[i], entities[j], dir, world));
                    }
                }
            }

            return collisionQueue;
        }

        private List<ICollisionInstance> CalcBlockCollisions<T>(List<T> entities, int[,] tiles) where T : ICollidable, IEntity
        {
            List<ICollisionInstance> collisionQueue = new List<ICollisionInstance>();

            foreach (T entity in entities)
            {
                if(entity == null) { continue; }
                for (int x = (int)Math.Floor(entity.Position.X) - 1; x < (int)Math.Ceiling(entity.Position.X + entity.Width) + 1; x++)
                {
                    for (int y = (int)Math.Floor(entity.Position.Y) - 1; y < (int)Math.Ceiling(entity.Position.Y + entity.Height) + 1; y++)
                    {
                        if (x < 0 || y < 0 || x >= tiles.GetLength(0) || y >= tiles.GetLength(1) || tiles[x, y] == 0) { continue; }
                        
                        Direction dir = entity.BoundingPolygon.DoesIntersect(new Collision.BoundingBox(x, y, 1, 1));

                        if (dir != Direction.None)
                        {
                            float lastX = entity.Position.X - entity.Velocity.X;
                            float lastY = entity.Position.Y - entity.Velocity.Y;

                            if (lastX + entity.Width <= x) { dir = Direction.Right; }
                            else if (lastX >= x + 1) { dir = Direction.Left; }
                            else if (lastY + entity.Height <= y) { dir = Direction.Up; }
                            else if (lastY >= y + 1) { dir = Direction.Down; }

                            collisionQueue.Add(new BlockCollisionInstance(x, y, Block.GetByID(tiles[x, y]), entity, dir, world));
                        }
                        if (dir == Direction.Down && entity is IEntity) { (entity as IEntity).IsGrounded = true; }
                    }
                }
            }

            return collisionQueue;
        }



        private bool DoNextCollision(List<ICollisionInstance> todo, List<ICollisionInstance> done)
        {
            todo.Sort();
            bool didIt = false;
            foreach (ICollisionInstance inst in todo)
            {
                if (!done.Contains(inst))
                {
                    inst.DoCollision();
                    done.Add(inst);
                    didIt = true;
                    break;
                }
            }
            return didIt;
        }
    }
}
