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

namespace SuperMarioYeah.Collectable
{
    public abstract class AbstractCollectable : AbstractEntity, IEntityCollidable, ICollectable
    {
        public override float Width { get { return (float)sprite.Bounds.X / World.BLOCK_SIZE; } }
        public override float Height { get { return (float)sprite.Bounds.Y / World.BLOCK_SIZE; } }
        public override bool IsTransitioning { get { return false; } }

        public IPolygon BoundingPolygon { get => new Collision.BoundingBox(Position.X, Position.Y, Width, Height); }

        protected AbstractCollectable(ISprite sprite, Vector2 position) : base(sprite, position) {  }

        public virtual void OnCollision(ICollidable obj, Direction dir, World world) {  }
    }
}