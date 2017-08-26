using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Collectable;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Enemies
{
    public class EnemyKoopa : AbstractEnemy
    {
        public const float SHELL_SPEED = ENEMY_SPEED * 7;
        public enum KoopaState { Walking, ShellIdle, ShellSpinning }
        public KoopaState State;

        public static readonly ISprite walkLeft
            = new AnimatedSprite(Textures.KoopaWalkingLeft, rows: 1, cols: 2, frameTime: 10);
        public static readonly ISprite walkRight
            = new AnimatedSprite(Textures.KoopaWalkingRight, rows: 1, cols: 2, frameTime: 10);
        public static readonly ISprite shellIdle
            = new StaticSprite(Textures.ShellGreenIdle);
        public static readonly ISprite shellSpinning
            = new AnimatedSprite(Textures.ShellGreenSpinning, rows: 3, cols: 1, frameTime: 6);

        public static int shellIdleTimer;

        public EnemyKoopa(Vector2 position) : base(walkLeft, position, deathTime: 10) { }

        public override void Update()
        {
            base.Update();

            switch (State)
            {
                case KoopaState.Walking:
                    if (Velocity.X > 0) { sprite = walkRight; }
                    if (Velocity.X < 0) { sprite = walkLeft; }
                    break;
                case KoopaState.ShellIdle:
                    sprite = shellIdle;
                    if (shellIdleTimer > 0)
                    {
                        shellIdleTimer--;
                    }
                    else
                    {
                        TransitionWalking();
                    }
                    break;
                case KoopaState.ShellSpinning:
                    sprite = shellSpinning;
                    break;
                default:
                    break;
            }
        }

        // the transition from walking to idle shell
        public override void TakeDamage()
        {
            sprite = shellIdle;
            Kill();
        }

        // OnCollision overwritten to account for shell logic, may need refactored
        public override void OnCollision(ICollidable obj, Direction dir, World world)
        {

            if (obj is Block && dir.IsHorizontal())
            {
                Velocity = new Vector2(-Velocity.X , Velocity.Y);
            }
            else
            {
                switch (State)
                {
                    case KoopaState.Walking:
                        if (obj is Mario)
                        {
                            if ((obj as Mario).StarTimer > 0) { TakeDamage(); }
                            else if (dir.Equals(Direction.Up)) { TransitionShellIdle(); }
                        }
                        break;

                    case KoopaState.ShellIdle:
                        if (obj is Mario)
                        {
                            Mario mario = obj as Mario;
                            Vector2 pos, vel;

                            if (mario.Position.X > Position.X)
                            {//move shell left
                                vel = new Vector2(-SHELL_SPEED, 0);
                                pos = new Vector2(Position.X - mario.Width, Position.Y);
                            }
                            else
                            {//move shell right
                                vel = new Vector2(SHELL_SPEED, 0);
                                pos = new Vector2(Position.X + mario.Width, Position.Y);
                            }

                            TransitionShellSpinning(pos, vel);
                        }
                        break;

                    case KoopaState.ShellSpinning:
                        if (obj is Mario)
                        {
                            if ((obj as Mario).StarTimer > 0) { TakeDamage(); }
                            else if (dir.Equals(Direction.Up)) { TransitionShellIdle(); }
                            else { (obj as Mario).TakeDamage(); }
                        }
                        else if (obj is AbstractEnemy)
                        {
                            (obj as AbstractEnemy).TakeDamage();
                        }
                        break;

                    default:
                        break;
                }
            }

        }

        private void TransitionShellIdle()
        {
            State = KoopaState.ShellIdle;
            Velocity = new Vector2(0, 0);
            shellIdleTimer = 280;
        }
        private void TransitionShellSpinning(Vector2 pos, Vector2 vel)
        {
            State = KoopaState.ShellSpinning;
            Position = pos;
            Velocity = vel;
        }
        private void TransitionWalking()
        {
            State = KoopaState.Walking;
            Velocity = new Vector2(ENEMY_SPEED,0);

        }

        public override IEntity Clone()
        {
            EnemyKoopa fred = new EnemyKoopa(Position);
            fred.Velocity = Velocity;
            fred.State = State;
            return fred;
        }
    }
}
