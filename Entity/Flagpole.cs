using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Collectable
{
    public class Flagpole : AbstractEntity, IEntityCollidable
    {
        public Flagpole(Vector2 position) : base(new StaticSprite(Textures.Flagpole), position) { }


        public IPolygon BoundingPolygon
        {
            get
            {
                return new Collision.BoundingBox(
                    Position.X,
                    Position.Y,
                    (float)sprite.Bounds.X / World.BLOCK_SIZE,
                    (float)sprite.Bounds.Y / World.BLOCK_SIZE);

            }
        }

        public void OnCollision(ICollidable obj, Direction dir, World world)
        {
            if(obj is Mario)
            {
                world.EndLevel(obj as Mario);
            }
        }

        public override bool IsTransitioning { get { return false; } }
        public override IEntity Clone() => new Flagpole(Position);
    }
}

