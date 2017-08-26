using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Collectable
{
    public class Coin : AbstractCollectable
    {
        public override bool IsGravitable { get => false; set { } }

        public Coin(Vector2 position) : base(new StaticSprite(Textures.Coin), position) {  }

        public override IEntity Clone() => new Coin(Position);
    }
}

