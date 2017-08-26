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
    public class Fireflower : AbstractCollectable, IPowerUp
    {
        public Fireflower(Vector2 position) : base(new StaticSprite(Textures.FireFlower), position) {  }

        public override IEntity Clone() => new Fireflower(Position);
    }
}

