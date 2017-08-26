using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperMarioYeah.Sprites;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Entities;


namespace SuperMarioYeah.Collectable
{
    public class Gun : AbstractCollectable, IPowerUp
    {
        public Gun(Vector2 position) : base(new StaticSprite(Textures.PowerupGun), position) {  }

        public override IEntity Clone()
        {
            return new Gun(this.Position);
        }
    }
}

