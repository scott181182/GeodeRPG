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

namespace SuperMarioYeah.Entities
{
    public class Flag : AbstractEntity
    {
        public Flag(Vector2 position) : base(new StaticSprite(Textures.Flag), position)
        {
            this.IsGravitable = false;

        }
        
        public override bool IsTransitioning { get { return false; } }

        public override IEntity Clone() => new Flag(Position);
    }
}

