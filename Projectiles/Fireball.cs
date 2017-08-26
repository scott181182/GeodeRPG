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

namespace SuperMarioYeah.Projectiles
{
    public class Fireball : AbstractProjectile
    {
        public const float X_VELOCITY = 0.2f;
        public const float Y_VELOCITY = 0.1f;

        public override bool IsGrounded { get { return false; } set {  } }

        public Fireball(Vector2 position, bool facingRight) : base(
            new AnimatedSprite(Textures.ProjectileFireball, rows: 1, cols: 4, frameTime: 3), position)
        {
            float xVelocity = X_VELOCITY * (facingRight ? 1 : -1);
            Velocity = new Vector2(xVelocity, -Y_VELOCITY);
        }

        public override void OnCollision(ICollidable obj, Direction dir, World world)
        {
            if(obj is Realm.Blocks.Block)
            {
                switch (dir)
                {
                    case Direction.Down:
                        Velocity = new Vector2(Velocity.X, Y_VELOCITY);
                        break;
                    case Direction.Up:
                        Velocity = new Vector2(Velocity.X, -Y_VELOCITY);
                        break;
                    case Direction.Left:
                    case Direction.Right: Kill(); break;
                }
            }
            else if (obj is AbstractEnemy && (obj as AbstractEnemy).IsAlive)
            {
                AbstractEnemy enemy = (AbstractEnemy) obj;
                enemy.TakeDamage();
                Kill();
                world.EarnPoints(100);
            }

        }

        public override IEntity Clone()
        {
            Fireball fred = new Fireball(Position, Velocity.X > 0);
            fred.Velocity = Velocity;
            return fred;
        }
    }
}
