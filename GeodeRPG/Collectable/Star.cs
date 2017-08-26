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
using SuperMarioYeah.Entities;
using SuperMarioYeah.Sprites;

namespace SuperMarioYeah.Collectable
{
    public class Star : AbstractCollectable, IPowerUp
    {
        public Star(Vector2 position) : base(new StaticSprite(Textures.Star), position) {  }

        public override IEntity Clone() => new Star(Position);
    }

}

