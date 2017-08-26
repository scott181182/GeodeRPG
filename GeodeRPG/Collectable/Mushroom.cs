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
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Collectable
{
    public class Mushroom : AbstractCollectable, IPowerUp
    {
        public const float X_VELOCITY = 0.075f;

        public Mushroom(Vector2 position) : base(new StaticSprite(Textures.RedMushroom), position)
        {
            Velocity = new Vector2(X_VELOCITY, 0);
        }

        public override void OnCollision(ICollidable obj, Direction dir, World world)
        {
            if(dir.IsHorizontal() && obj is Block)
            {
                Velocity = new Vector2(dir == Direction.Right ? -X_VELOCITY : X_VELOCITY, Velocity.Y);
            }
        }

        public override IEntity Clone() => new Mushroom(Position);
    }
}

