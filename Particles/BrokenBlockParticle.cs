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
    public class BrokenBlockParticle : AbstractEntity
    {
        private static Random rng = new Random();
        
        public override bool IsGrounded { get { return false; } set {  } }
        public override bool IsTransitioning { get { return false; } }

        public BrokenBlockParticle(int x, int y) : base(
            new StaticSprite(Textures.ParticleBrokenBlock), new Vector2(x + 0.5f, y + 0.5f))
        {
            Velocity = RandomDirection();
        }
        public BrokenBlockParticle(Vector2 position) : base(
            new StaticSprite(Textures.ParticleBrokenBlock), position)
        {
            Velocity = RandomDirection();
        }

        private static Vector2 RandomDirection()
        {
            float x = (float)rng.NextDouble() / 4 - 0.125f;
            float randY = (float)rng.NextDouble();
            float y = (1.5f - randY * randY) / 5;
            return new Vector2(x, y);
        }

        public override void Update()
        {
            base.Update();

            if(Position.Y + Height < 0) { Kill(); }
        }

        public override IEntity Clone()
        {
            BrokenBlockParticle fred = new BrokenBlockParticle(Position);
            fred.Velocity = Velocity;
            return fred;
        }
    }
}
