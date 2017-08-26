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
    public class OneUp : AbstractCollectable
    {

        public OneUp(Vector2 position) : base(new StaticSprite(Textures.GreenMushroom), position) {  }

        public override IEntity Clone() => new Mushroom(Position);
    }

}

