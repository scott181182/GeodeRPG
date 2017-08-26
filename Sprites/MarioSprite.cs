using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Sprites
{
    public class MarioSprite : ISprite
    {
        public bool Visible { get; set; }

        public Point Bounds { get; set; }

        private Mario mario;
        private ISprite[][][] MarioSprites;
        private ISprite currentSprite;
        private ISprite transitionSprite1;
        private ISprite transitionSprite2;
        private Mario.MarioState marioState1;
        private Mario.MarioState marioState2; 
        private int frameCounter;

        public MarioSprite(Mario mario)
        {
            LoadSprites();
            int direction = mario.FacingRight ? 1 : 0;
            currentSprite = MarioSprites[(int)mario.State][direction][(int)mario.ActionState];
            this.mario = mario;
            this.Visible = true;
            this.frameCounter = 0;
        }

        public void Update()
        {
            // Select sprite based on state, Right/left, ActionState
            int direction = mario.FacingRight ? 1 : 0;
            currentSprite = MarioSprites[(int)mario.State][direction][(int)mario.ActionState];
            transitionSprite1 = MarioSprites[(int)marioState1][direction][(int)mario.ActionState];
            transitionSprite2 = MarioSprites[(int)marioState2][direction][(int)mario.ActionState];
            this.currentSprite.Update();
            this.transitionSprite1.Update();
            this.transitionSprite2.Update();
            Bounds = currentSprite.Bounds;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, float scale = 1, bool chain = false)
        {
            Draw(spriteBatch, location, Color.White, scale, chain);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, float scale = 1, bool chain = false)
        {
            if(!Visible) { return; }
            if (mario.PowerUpTimer > 0)     { DrawTransitioning (spriteBatch, location, scale, chain); }
            else if (mario.StarTimer > 0)   { DrawWithStar      (spriteBatch, location, scale, chain); }
            else if (mario.DamageTimer > 0) { DrawTakingDamage  (spriteBatch, location, scale, chain); }
            else                            { currentSprite.Draw(spriteBatch, location, color, scale, chain); }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color, bool chain = false)
        {
            if (!Visible) { return; }
            currentSprite.Draw(spriteBatch, destination, color, chain);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle destination, bool chain = false)
        {
            if (!Visible) { return; }
            currentSprite.Draw(spriteBatch, destination, chain);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle destination, float rotation, Vector2 position)
        {
            if (!Visible) { return; }
            //spriteBatch.Draw(currentSprite, destination, null, Color.White, rotation, position, SpriteEffects.None, 0f);
        }

        private void DrawTakingDamage(SpriteBatch spriteBatch, Vector2 location, float scale = 1, bool chain = false)
        {
            if (frameCounter > 10) { frameCounter = 0; }
            else if (frameCounter > 5) { frameCounter++; }
            else
            {
                frameCounter++;
                currentSprite.Draw(spriteBatch, location, scale, chain);
            }
        }

        private void DrawWithStar(SpriteBatch spriteBatch, Vector2 location, float scale = 1, bool chain = false)
        {
            if (frameCounter > 15)
            {
                frameCounter = 0;
                currentSprite.Draw(spriteBatch, location, Color.YellowGreen, scale, chain);
            }
            else if (frameCounter > 10)
            {
                frameCounter++;
                currentSprite.Draw(spriteBatch, location, Color.YellowGreen, scale, chain);
            }
            else if (frameCounter > 5)
            {
                frameCounter++;
                currentSprite.Draw(spriteBatch, location, Color.Red, scale, chain);
            }
            else
            {
                frameCounter++;
                currentSprite.Draw(spriteBatch, location, Color.Violet, scale, chain);
            }
        }

        private void DrawTransitioning(SpriteBatch spriteBatch, Vector2 location, float scale = 1, bool chain = false)
        {
            int direction = mario.FacingRight ? 1 : 0;
            transitionSprite1 = MarioSprites[(int)marioState1][direction][(int)mario.ActionState];
            transitionSprite2 = MarioSprites[(int)marioState2][direction][(int)mario.ActionState];

            if (frameCounter > 10)
            {
                frameCounter = 0;
                transitionSprite1.Draw(spriteBatch, location, scale, chain);
            }
            else if (frameCounter > 5)
            {
                frameCounter++;
                transitionSprite1.Draw(spriteBatch, location, scale, chain);
            }
            else
            {
                frameCounter++;
                transitionSprite2.Draw(spriteBatch, location, scale, chain);
            }
        }

        public void SetTransitionStates(Mario.MarioState newState)
        {
            marioState1 = mario.State;
            int direction = mario.FacingRight ? 1 : 0;
            marioState2 = newState;
        }

        private void LoadSprites()
        {
            int dead      = (int)Mario.MarioState.Dead;
            int small     = (int)Mario.MarioState.Small;
            int big       = (int)Mario.MarioState.Big;
            int fire      = (int)Mario.MarioState.Fire;
            int tnt       = (int)Mario.MarioState.TNT;
            int gun       = (int)Mario.MarioState.Gun;

            int idle      = (int)Mario.MarioActionState.Idle;
            int crouching = (int)Mario.MarioActionState.Crouching;
            int jumping   = (int)Mario.MarioActionState.Jumping;
            int running   = (int)Mario.MarioActionState.Running;

            int left  = 0;
            int right = 1;
            
            // initialize sprite types for all mario states
            // multidimensional array MarioSprites[(int)MarioState][Left (0)/Right (1)][(int)MarioActionState] 
            MarioSprites = new ISprite[Enum.GetValues(typeof(Mario.MarioState)).Length][][];
            foreach (Mario.MarioState state in Enum.GetValues(typeof(Mario.MarioState)))
            {
                MarioSprites[(int)state] = new ISprite[2][];
                MarioSprites[(int)state][left] = new ISprite[Enum.GetValues(typeof(Mario.MarioActionState)).Length];
                MarioSprites[(int)state][right] = new ISprite[Enum.GetValues(typeof(Mario.MarioActionState)).Length];
            }


            MarioSprites[small][left][idle]       = new StaticSprite  (Textures.MarioSmallIdleLeft);
            MarioSprites[small][right][idle]      = new StaticSprite  (Textures.MarioSmallIdleRight);
            MarioSprites[small][left][crouching]  = new StaticSprite  (Textures.MarioSmallIdleLeft);
            MarioSprites[small][right][crouching] = new StaticSprite  (Textures.MarioSmallIdleRight);
            MarioSprites[small][left][jumping]    = new StaticSprite  (Textures.MarioSmallJumpingLeft);
            MarioSprites[small][right][jumping]   = new StaticSprite  (Textures.MarioSmallJumpingRight);
            MarioSprites[small][left][running]    = new AnimatedSprite(Textures.MarioSmallRunningLeft, 1, 2, 0, -1, 6);
            MarioSprites[small][right][running]   = new AnimatedSprite(Textures.MarioSmallRunningRight, 1, 2, 0, -1, 6);

            MarioSprites[big][left][idle]       = new StaticSprite  (Textures.MarioBigIdleLeft);
            MarioSprites[big][right][idle]      = new StaticSprite  (Textures.MarioBigIdleRight);
            MarioSprites[big][left][crouching]  = new StaticSprite  (Textures.MarioBigCrouchingLeft);
            MarioSprites[big][right][crouching] = new StaticSprite  (Textures.MarioBigCrouchingRight);
            MarioSprites[big][left][jumping]    = new StaticSprite  (Textures.MarioBigJumpingLeft);
            MarioSprites[big][right][jumping]   = new StaticSprite  (Textures.MarioBigJumpingRight);
            MarioSprites[big][left][running]    = new AnimatedSprite(Textures.MarioBigRunningLeft, 1, 3, 0, -1, 5);
            MarioSprites[big][right][running]   = new AnimatedSprite(Textures.MarioBigRunningRight, 1, 3, 0, -1, 5);

            MarioSprites[fire][left][idle]       = new StaticSprite  (Textures.MarioFireIdleLeft);
            MarioSprites[fire][right][idle]      = new StaticSprite  (Textures.MarioFireIdleRight);
            MarioSprites[fire][left][crouching]  = new StaticSprite  (Textures.MarioFireCrouchingLeft);
            MarioSprites[fire][right][crouching] = new StaticSprite  (Textures.MarioFireCrouchingRight);
            MarioSprites[fire][left][jumping]    = new StaticSprite  (Textures.MarioFireJumpingLeft);
            MarioSprites[fire][right][jumping]   = new StaticSprite  (Textures.MarioFireJumpingRight);
            MarioSprites[fire][left][running]    = new AnimatedSprite(Textures.MarioFireRunningLeft, 1, 3, 0, -1, 5);
            MarioSprites[fire][right][running]   = new AnimatedSprite(Textures.MarioFireRunningRight, 1, 3, 0, -1, 5);

            MarioSprites[tnt][left][idle]       = new StaticSprite  (Textures.MarioTNTIdleLeft);
            MarioSprites[tnt][right][idle]      = new StaticSprite  (Textures.MarioTNTIdleRight);
            MarioSprites[tnt][left][crouching]  = new StaticSprite  (Textures.MarioTNTCrouchingLeft);
            MarioSprites[tnt][right][crouching] = new StaticSprite  (Textures.MarioTNTCrouchingRight);
            MarioSprites[tnt][left][jumping]    = new StaticSprite  (Textures.MarioTNTJumpingLeft);
            MarioSprites[tnt][right][jumping]   = new StaticSprite  (Textures.MarioTNTJumpingRight);
            MarioSprites[tnt][left][running]    = new AnimatedSprite(Textures.MarioTNTRunningLeft, 1, 3, 0, -1, 5);
            MarioSprites[tnt][right][running]   = new AnimatedSprite(Textures.MarioTNTRunningRight, 1, 3, 0, -1, 5);

            MarioSprites[gun][left][idle]       = new StaticSprite  (Textures.MarioGunIdleLeft);
            MarioSprites[gun][right][idle]      = new StaticSprite  (Textures.MarioGunIdleRight);
            MarioSprites[gun][left][crouching]  = new StaticSprite  (Textures.MarioGunCrouchingLeft);
            MarioSprites[gun][right][crouching] = new StaticSprite  (Textures.MarioGunCrouchingRight);
            MarioSprites[gun][left][jumping]    = new AnimatedSprite(Textures.MarioGunJumpingLeft,  rows: 1, cols: 4, frameTime: 5);
            MarioSprites[gun][right][jumping]   = new AnimatedSprite(Textures.MarioGunJumpingRight, rows: 1, cols: 4, frameTime: 5);
            MarioSprites[gun][left][running]    = new AnimatedSprite(Textures.MarioGunRunningLeft,  rows: 1, cols: 3, frameTime: 5);
            MarioSprites[gun][right][running]   = new AnimatedSprite(Textures.MarioGunRunningRight, rows: 1, cols: 3, frameTime: 5);

            MarioSprites[dead][left][idle]       = new StaticSprite(Textures.MarioDead);
            MarioSprites[dead][right][idle]      = new StaticSprite(Textures.MarioDead);
            MarioSprites[dead][left][crouching]  = new StaticSprite(Textures.MarioDead);
            MarioSprites[dead][right][crouching] = new StaticSprite(Textures.MarioDead);
            MarioSprites[dead][left][jumping]    = new StaticSprite(Textures.MarioDead);
            MarioSprites[dead][right][jumping]   = new StaticSprite(Textures.MarioDead);
            MarioSprites[dead][left][running]    = new StaticSprite(Textures.MarioDead);
            MarioSprites[dead][right][running]   = new StaticSprite(Textures.MarioDead);
        }

        public ISprite GetIdleSprite()
        {
            return MarioSprites[(int)mario.State][mario.FacingRight ? 1 : 0][(int)Mario.MarioActionState.Idle];
        }
    }
}
