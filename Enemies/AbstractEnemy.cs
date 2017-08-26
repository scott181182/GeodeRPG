using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Realm.Blocks;
using SuperMarioYeah.Sprites;

namespace SuperMarioYeah.Enemies
{
    public abstract class AbstractEnemy : AbstractEntity, IEnemy, IEntityCollidable
    {
        public const float ENEMY_SPEED = .02f;
        
        private int deathTimer = 0;
        private int DeathTime;

        public override void Kill()
        {
            base.Kill();
            deathTimer = DeathTime;
            Velocity = Vector2.Zero;
        }

        public override bool IsTransitioning { get { return !IsAlive && deathTimer > 0; } }

        public override float Width { get { return this.BoundingPolygon.Width; } }
        public override float Height { get { return this.BoundingPolygon.Height; } }
        public IPolygon BoundingPolygon
        {
            get
            {
                return new Collision.BoundingBox(
                    Position.X,
                    Position.Y,
                    (float)sprite.Bounds.X / World.BLOCK_SIZE,
                    (float)sprite.Bounds.Y / World.BLOCK_SIZE
                );
            }
        }


        protected AbstractEnemy(ISprite sprite, Vector2 position, int deathTime) : base(sprite, position)
        {
            Velocity = new Vector2(-ENEMY_SPEED, 0);
            DeathTime = deathTime;
        }

        public override void Update()
        {
            base.Update();

            if(Position.X <= 0) { Velocity = new Vector2(-Velocity.X, Velocity.Y); }
            if(deathTimer > 0) { deathTimer -= 1; }
        }

        public virtual void OnCollision(ICollidable obj, Direction dir, World world)
        {
            if (obj is Block && dir.IsHorizontal())
            {
                Velocity = new Vector2(ENEMY_SPEED * (dir == Direction.Left ? 1 : -1), Velocity.Y);
            }
            else if (obj is Mario)
            {
                if (dir.Equals(Direction.Up) || (obj as Mario).StarTimer > 0) { TakeDamage(); }
            }
        }

        public abstract void TakeDamage();
    }
}