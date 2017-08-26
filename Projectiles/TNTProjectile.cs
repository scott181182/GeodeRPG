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
using SuperMarioYeah.Realm.Blocks;
using SuperMarioYeah.Particles;

namespace SuperMarioYeah.Projectiles
{
    public class TNTProjectile : AbstractProjectile
    {
        public const float X_VELOCITY = 0.2f;
        public const float Y_VELOCITY = 0.3f;
        public const float RESTITUTION = 0.5f;
        public const float EPSILON = 0.01f;
        public const int RADIUS = 4;

        private int fuse;
        private World world;
        private Random rng;

        public override bool IsGrounded { get { return false; } set {  } }

        public TNTProjectile(Vector2 position, bool facingRight, World world) : base(
            new AnimatedSprite(Textures.ProjectileTNT, rows: 1, cols: 2, frameTime: 10), position)
        {
            float xVelocity = X_VELOCITY * (facingRight ? 1 : -1);
            Velocity = new Vector2(xVelocity, Y_VELOCITY);
            fuse = 30 * 3;
            rng = new Random();
            this.world = world;
        }

        public override void Update()
        {
            base.Update();

            if (fuse > 0) { fuse--; }
            if (fuse == 0) { Detonate(); }
        }

        public override void OnCollision(ICollidable obj, Direction dir, World world)
        {
            if(obj is Realm.Blocks.Block && (Math.Abs(Velocity.X) > 0 || Math.Abs(Velocity.Y) > 0))
            {
                if(dir.IsVertical()) { Velocity = new Vector2(Velocity.X, -Velocity.Y) * RESTITUTION; }
                else { Velocity = new Vector2(-Velocity.X, Velocity.Y) * RESTITUTION; }

                if (Math.Abs(Velocity.X) < EPSILON) { Velocity = new Vector2(0, Velocity.Y); }
                if (Math.Abs(Velocity.Y) < EPSILON) { Velocity = new Vector2(Velocity.X, 0); }
            }
        }
        
        private void Detonate()
        {
            float cx = Position.X + Width / 2;
            float cy = Position.Y + Width / 2;
            for (int x = (int)Math.Floor(cx - RADIUS); x < (int)Math.Ceiling(cx + RADIUS); x++)
            {
                for (int y = (int)Math.Floor(cy - RADIUS); y < (int)Math.Ceiling(cy + RADIUS); y++)
                {
                    float dx = x - cx;
                    float dy = y - cy;
                    double distance = Math.Sqrt(dx * dx + dy * dy);
                    if(distance > RADIUS) { continue; }

                    double probability = -(1.0 / 12) * (distance + RADIUS) * (distance - RADIUS);
                    if(rng.NextDouble() < probability)
                    {
                        if (world.GetBlockAt(x, y) == Block.brickBlock) { world.SetTileAt(x, y, 0); }
                        foreach(IEntity entity in world.GetEntitiesAt<IEntity>(x, y))
                        {
                            if (entity is IEnemy) { (entity as IEnemy).TakeDamage(); }
                            else if (entity is Mario) { (entity as Mario).TakeDamage(); }
                            else if(!(entity is ITileEntity || entity is Flag)) { entity.Kill(); }
                        }
                    }
                }
            }
            for(int i = 0; i < 10; i++) { world.AddEntity(new BrokenBlockParticle(RandomPosition())); }

            Sounds.SoundLoader.Explode(Position);
            Kill();
        }
        private Vector2 RandomPosition()
        {
            return new Vector2((float)rng.NextDouble() * 2 - 1, (float)rng.NextDouble() * 2 - 1) + Position;
        }

        public override IEntity Clone() => new TNTProjectile(Position, Velocity.X > 0, world)
            {
                Velocity = Velocity,
                fuse = fuse
            };
    }
}
