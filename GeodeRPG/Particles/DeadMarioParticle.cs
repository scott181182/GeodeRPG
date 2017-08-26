using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Sprites;

namespace SuperMarioYeah.Particles
{
    public class DeadMarioParticle : AbstractEntity
    {
        public override bool IsTransitioning => false;

        public DeadMarioParticle(Vector2 position) : base(new StaticSprite(Textures.MarioDead), position)
        {
            Velocity = new Vector2(0, 0.5f);
        }

        public override IEntity Clone()
        {
            DeadMarioParticle fred = new DeadMarioParticle(Position);
            fred.Velocity = Velocity;
            return fred;
        }
    }
}
