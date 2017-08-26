using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Sprites;

namespace SuperMarioYeah.Projectiles
{
    public abstract class AbstractProjectile : AbstractEntity, IProjectile
    {
        public override float Width { get { return BoundingPolygon.Width;  } }
        public override float Height { get { return BoundingPolygon.Height; } }
        public override bool IsTransitioning { get { return false; } }

        public IPolygon BoundingPolygon
        {
            get {
                return new Collision.BoundingBox(
                    Position.X,
                    Position.Y,
                    (float)sprite.Bounds.X / World.BLOCK_SIZE,
                    (float)sprite.Bounds.Y / World.BLOCK_SIZE
                );
            }
        }

        protected AbstractProjectile(ISprite sprite, Vector2 position) : base(sprite, position) {  }

        public abstract void OnCollision(ICollidable obj, Direction dir, World world);
    }
}