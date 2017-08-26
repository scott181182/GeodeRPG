using Microsoft.Xna.Framework;
using SuperMarioYeah.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Particles
{
    public class CoinParticle : AbstractEntity
    {
        public override bool IsGrounded { get { return false; } set { } }
        public override bool IsTransitioning { get { return false; } }

 

        public CoinParticle(int x, int y) : base(
            new StaticSprite(Textures.Coin), new Vector2(x , y + 0.5f))
        {
            Sounds.SoundLoader.Coin(new Vector2(x, y));
            Velocity = new Vector2(0,0.2f);

        }
    

        public override void Update()
        {
            base.Update();

            if(Velocity.Y < 0) { Kill(); }
        }

        public override IEntity Clone()
        {
            return new CoinParticle(0, 0)
            {
                Position = Position,
                Velocity = Velocity
            };
        }
    }
}
