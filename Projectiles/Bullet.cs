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
    public class Bullet : AbstractProjectile
    {
        public const float X_VELOCITY = 1;

        public override bool IsGrounded { get { return false; } set {  } }

        public Bullet(Vector2 position, bool facingRight) : base(
            new AnimatedSprite(Textures.ProjectileFireball, rows: 1, cols: 4, frameTime: 3), position)
        {
            float xVelocity = X_VELOCITY * (facingRight ? 1 : -1);
            Velocity = new Vector2(xVelocity, 0);
            this.IsGravitable = false;
        }

        public override void OnCollision(ICollidable obj, Direction dir, World world)
        {
            if(obj is Realm.Blocks.Block)
            {
                Kill();
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
            return new Bullet(this.Position, this.Velocity.X == X_VELOCITY);
        }
    }
}
