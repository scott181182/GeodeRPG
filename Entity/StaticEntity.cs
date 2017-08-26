using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Collectable;

namespace SuperMarioYeah.Entities
{
    /**
     * Just here as an example, pretty much the simplist implementation of an IEntity that doesn't move, just renders
     */
    public class StaticEntity : AbstractEntity
    {
        public override float Width { get { return (float)sprite.Bounds.X / World.BLOCK_SIZE; } }
        public override float Height { get { return (float)sprite.Bounds.Y / World.BLOCK_SIZE; } }
        public override bool IsTransitioning { get { return false; } }

        public StaticEntity(ISprite sprite, Vector2 position) : base(sprite, position) { }

        public override IEntity Clone() => new StaticEntity(sprite, Position);
    }
}