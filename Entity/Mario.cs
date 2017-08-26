using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Collectable;
using SuperMarioYeah.Projectiles;
using SuperMarioYeah.Realm.TileEntity;
using SuperMarioYeah.Particles;
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Entities
{
    public class Mario : AbstractEntity, IEntityCollidable
    {
        public enum MarioState { Dead = 0, Small, Big, Fire, TNT, Gun }
        public enum MarioActionState { Idle = 0, Jumping, Crouching, Running }

        public MarioState State { get; set; }
        public MarioActionState ActionState { get; set; }

        public int JumpNumber;
        public int JumpTimer;

        private World world;

        private int pointStreak;
        public bool FacingRight;
        public int StarTimer { get; private set; }
        public int DamageTimer { get; private set; }
        public int PowerUpTimer { get; private set; }


        public override float Width { get { return BoundingPolygon.Width; } }
        public override float Height { get { return BoundingPolygon.Height; } }
        public IPolygon BoundingPolygon {
            get {
                return new Collision.BoundingBox(
                    Position.X,
                    Position.Y,
                    (float)sprite.Bounds.X / World.BLOCK_SIZE,
                    (float)sprite.Bounds.Y / World.BLOCK_SIZE);
            }
        }
        public override bool IsTransitioning { get { return DamageTimer > 0 || PowerUpTimer > 0; } }

        public Mario(Vector2 position, World world) : base(position)
        {
            this.sprite = new MarioSprite(this);

            this.FacingRight = true;
            this.State = MarioState.Small;
            this.ActionState = MarioActionState.Idle;

            this.pointStreak = 0;
            StarTimer = DamageTimer = PowerUpTimer = 0;
            this.world = world;
            this.JumpNumber = 0;
        }

        public override void Update()
        {
            base.Update();

            if (StarTimer > 0) { StarTimer--; }
            if (DamageTimer > 0) { DamageTimer--; }
            if (PowerUpTimer > 0) { PowerUpTimer--; }
            if (JumpTimer > 0 && IsGrounded) { JumpTimer--; }
            if (JumpTimer == 0) { JumpNumber = 0;  }
            if(Position.Y < 0) { Kill(); return; }
            if (IsGrounded) { pointStreak = 0; }
            

            if(ActionState == MarioActionState.Crouching)
            {
                int leftCoord = (int)Math.Floor(Position.X);
                int rightCoord = (int)Math.Ceiling(Position.X + Width);
                int yCoord = (int)Math.Floor(Position.Y) - 1;

                PipeTileEntity te = world.GetTileEntityAt<PipeTileEntity>(leftCoord, yCoord);
                if (te != null) { GoThroughPipe(te, Direction.Down); }
                else
                {
                    te = world.GetTileEntityAt<PipeTileEntity>(rightCoord, yCoord);
                    if (te != null) { GoThroughPipe(te, Direction.Down); }
                }
            }

            UpdateStates();
        }
        public void GoThroughPipe(PipeTileEntity pte, Direction inDir)
        {
            if (!Visible) { return; }
            Visible = false;
            Direction outDir = Direction.None;
            int destx = pte.TransitionX;
            int desty = pte.TransitionY;
            if      (world.GetBlockAt(destx + 1, desty) is PipeBlock) { outDir = Direction.Left;  }
            else if (world.GetBlockAt(destx - 1, desty) is PipeBlock) { outDir = Direction.Right; }
            else if (world.GetBlockAt(destx, desty + 1) is PipeBlock) { outDir = Direction.Down;  }
            else if (world.GetBlockAt(destx, desty - 1) is PipeBlock) { outDir = Direction.Up;    }
            if (outDir != Direction.None)
            {
                world.AddEntity(new PipeMarioParticle(sprite as MarioSprite, Position, inDir, outDir, () =>
                {
                    world.CurrentRegion = world.Regions[pte.TransitionRegion];
                    Position = new Vector2(pte.TransitionX, pte.TransitionY);
                    return Position;
                }, () => { Visible = true; }));
            }
            else
            {
                world.AddEntity(new PipeMarioParticle(sprite as MarioSprite, Position, inDir, () =>
                {
                    world.CurrentRegion = world.Regions[pte.TransitionRegion];
                    Position = new Vector2(pte.TransitionX, pte.TransitionY);
                    Visible = true;
                }));
            }
            Sounds.SoundLoader.Pipe(Position);
        }

        private void UpdateStates()
        {
            if (!IsGrounded && Velocity.Y != 0) { ActionState = MarioActionState.Jumping; }
            else if (Velocity.X != 0) { ActionState = MarioActionState.Running; }
            else { ActionState = MarioActionState.Idle; }
        }



        public void Jump()
        {
            if (State != MarioState.Dead && IsGrounded)
            {
                if (JumpNumber == 0)
                {
                    ActionState = Mario.MarioActionState.Jumping;
                    IsGrounded = false;
                    Accelerate(0, 0.4f);

                    if (State == MarioState.Small) { Sounds.SoundLoader.JumpSmall(Position); }
                    else { Sounds.SoundLoader.JumpBig(Position); }

                    JumpNumber++;
                } else if (JumpNumber == 1)
                {
                    ActionState = Mario.MarioActionState.Jumping;
                    IsGrounded = false;
                    Accelerate(0, 0.5f);

                    if (State == MarioState.Small) { Sounds.SoundLoader.JumpSmall(Position); }
                    else { Sounds.SoundLoader.JumpBig(Position); }

                    JumpNumber++;
                } else if (JumpNumber == 2)
                {
                    ActionState = Mario.MarioActionState.Jumping;
                    IsGrounded = false;
                    Accelerate(0, 0.6f);

                    if (State == MarioState.Small) { Sounds.SoundLoader.JumpSmall(Position); }
                    else { Sounds.SoundLoader.JumpBig(Position); }

                    JumpNumber = 0;
                }
                JumpTimer = 5;
            }
        }

        public void UseAbility()
        {
            switch(State)
            {
                case MarioState.Fire: ShootFireball(); break;
                case MarioState.TNT: ShootTNT(); break;
                case MarioState.Gun: ShootGun(); break;
            }
        }
        private void ShootFireball()
        { 
            Fireball fireball = new Fireball(Position + new Vector2(0, 1), FacingRight);
            this.world.AddEntity(fireball);
            Sounds.SoundLoader.Fireball(Position);
        }
        private void ShootTNT()
        {
            TNTProjectile tnt = new TNTProjectile(Position + new Vector2(0, 1), FacingRight, world);
            this.world.AddEntity(tnt);
            Sounds.SoundLoader.Fireball(Position);
        }

        private void ShootGun()
        {
            Vector2 bulletPos = new Vector2();
            if (FacingRight)
            {
                bulletPos = Position + new Vector2(.5f, .7f);
                if (ActionState == MarioActionState.Crouching) bulletPos += new Vector2(1.5f, -.3f);
            }
            else
            {
                bulletPos = Position + new Vector2(-.2f, .7f);
                if (ActionState == MarioActionState.Crouching) bulletPos += new Vector2(-.2f, -.3f);
            }

            Bullet bullet = new Bullet(bulletPos, FacingRight);
            this.world.AddEntity(bullet);

            Sounds.SoundLoader.Fireball(Position);
        }


        public void OnCollision(ICollidable obj, Direction dir, World world)
        {
            if (State == MarioState.Dead) { return; }
            if (obj is AbstractEnemy && (obj as AbstractEnemy).IsAlive)
            {
                if ((dir == Direction.Down && DamageTimer == 0) || StarTimer > 0)
                {
                    world.EarnPoints(100 * 2 ^ pointStreak++);
                    this.Velocity = new Vector2(this.Velocity.X, Math.Abs(this.Velocity.Y));
                }
                else if (dir != Direction.Down && DamageTimer == 0)
                {
                    if (!(obj is EnemyKoopa && (obj as EnemyKoopa).State == EnemyKoopa.KoopaState.ShellIdle))
                    {
                        TakeDamage();
                    }
                }
            }
            else if(obj is ICollectable) { CollectItem(obj as ICollectable); }
        }
        private void CollectItem(ICollectable pu)
        {
            MarioSprite mSprite = sprite as MarioSprite;

            if (pu is Fireflower && State != MarioState.Fire)
            {
                mSprite.SetTransitionStates(MarioState.Fire);
                PowerUpTimer = 80;
                State = MarioState.Fire;

            }
            else if (pu is TNTPowerUp && State != MarioState.TNT)
            {
                mSprite.SetTransitionStates(MarioState.TNT);
                PowerUpTimer = 80;
                State = MarioState.TNT;
            }
            else if (pu is Gun && State != MarioState.Gun)
            {
                mSprite.SetTransitionStates(MarioState.Gun);
                PowerUpTimer = 90;
                State = MarioState.Gun;
            }
            else if (pu is Mushroom && State < MarioState.Big)
            {
                mSprite.SetTransitionStates(MarioState.Big);
                PowerUpTimer = 80;
                State = MarioState.Big;
            }
            else if (pu is Star) { StarTimer = 600; }
            else if (pu is Coin) { world.CollectCoin(pu as Coin); }
            else if (pu is OneUp) { world.Lives++; }

            pu.Kill();
        }



        public void TakeDamage()
        {
            if(DamageTimer > 0 || StarTimer > 0) { return; }
            if (State > MarioState.Big) { State = MarioState.Big; }
            else if (State == MarioState.Big) { State = MarioState.Small; }
            else if (State == MarioState.Small) { Kill(); }

            // set mario to not take damage for 2 seconds
            if (IsAlive) { DamageTimer = 120; }
        }
        public override void Kill()
        {
            base.Kill();

            State = MarioState.Dead;
            Sounds.SoundLoader.MarioDie(Position);
            world.State = WorldState.DYING;
            world.AddEntity(new Particles.DeadMarioParticle(Position));
        }
        public override IEntity Clone()
        {
            return new Mario(Position, world)
            {
                FacingRight = FacingRight,
                State = State,
                ActionState = ActionState,
                DamageTimer = DamageTimer,
                StarTimer = StarTimer,
                PowerUpTimer = PowerUpTimer,
                JumpTimer = JumpTimer,
                JumpNumber = JumpNumber,
                pointStreak = pointStreak,
                Visible = Visible
            };
        }
    }
}