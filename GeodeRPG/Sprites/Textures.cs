using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioYeah
{
    public static class Textures
    {
        /* Mario Textures */
        public static Texture2D MarioDead { get; private set; }
        // Mario Small
        public static Texture2D MarioSmallIdleRight    { get; private set; }
        public static Texture2D MarioSmallJumpingRight { get; private set; }
        public static Texture2D MarioSmallRunningRight { get; private set; }
        public static Texture2D MarioSmallIdleLeft     { get; private set; }
        public static Texture2D MarioSmallJumpingLeft  { get; private set; }
        public static Texture2D MarioSmallRunningLeft  { get; private set; }
        // Mario Big
        public static Texture2D MarioBigIdleRight      { get; private set; }
        public static Texture2D MarioBigJumpingRight   { get; private set; }
        public static Texture2D MarioBigRunningRight   { get; private set; }
        public static Texture2D MarioBigCrouchingRight { get; private set; }
        public static Texture2D MarioBigIdleLeft       { get; private set; }
        public static Texture2D MarioBigJumpingLeft    { get; private set; }
        public static Texture2D MarioBigRunningLeft    { get; private set; }
        public static Texture2D MarioBigCrouchingLeft  { get; private set; }
        // Mario Fire
        public static Texture2D MarioFireIdleRight      { get; private set; }
        public static Texture2D MarioFireJumpingRight   { get; private set; }
        public static Texture2D MarioFireRunningRight   { get; private set; }
        public static Texture2D MarioFireCrouchingRight { get; private set; }
        public static Texture2D MarioFireIdleLeft       { get; private set; }
        public static Texture2D MarioFireJumpingLeft    { get; private set; }
        public static Texture2D MarioFireRunningLeft    { get; private set; }
        public static Texture2D MarioFireCrouchingLeft  { get; private set; }
        // Mario TNT
        public static Texture2D MarioTNTIdleRight { get; private set; }
        public static Texture2D MarioTNTJumpingRight { get; private set; }
        public static Texture2D MarioTNTRunningRight { get; private set; }
        public static Texture2D MarioTNTCrouchingRight { get; private set; }
        public static Texture2D MarioTNTIdleLeft { get; private set; }
        public static Texture2D MarioTNTJumpingLeft { get; private set; }
        public static Texture2D MarioTNTRunningLeft { get; private set; }
        public static Texture2D MarioTNTCrouchingLeft { get; private set; }
        // Mario Gun
        public static Texture2D MarioGunIdleRight { get; private set; }
        public static Texture2D MarioGunJumpingRight { get; private set; }
        public static Texture2D MarioGunRunningRight { get; private set; }
        public static Texture2D MarioGunCrouchingRight { get; private set; }
        public static Texture2D MarioGunIdleLeft { get; private set; }
        public static Texture2D MarioGunJumpingLeft { get; private set; }
        public static Texture2D MarioGunRunningLeft { get; private set; }
        public static Texture2D MarioGunCrouchingLeft { get; private set; }



        /* Block Textures */
        public static Texture2D BlockBrick    { get; private set; }
        public static Texture2D BlockPlatform { get; private set; }
        public static Texture2D BlockCracked  { get; private set; }
        public static Texture2D BlockMystery  { get; private set; }
        public static Texture2D BlockUsed     { get; private set; }

        /* Pipe Textures */
        // Horizontal Pipes
        public static Texture2D PipeLeft        { get; private set; }
        public static Texture2D PipeRight       { get; private set; }
        public static Texture2D PipeSide        { get; private set; }
        public static Texture2D PipeLeftBottom  { get; private set; }
        public static Texture2D PipeLeftTop     { get; private set; }
        public static Texture2D PipeRightBottom { get; private set; }
        public static Texture2D PipeRightTop    { get; private set; }
        public static Texture2D PipeSideBottom  { get; private set; }
        public static Texture2D PipeSideTop     { get; private set; }
        // Vertical Pipes
        public static Texture2D PipeUp         { get; private set; }
        public static Texture2D PipeTop        { get; private set; }
        public static Texture2D PipeUpLeftTop  { get; private set; }
        public static Texture2D PipeUpRightTop { get; private set; }
        public static Texture2D PipeUpLeft     { get; private set; }
        public static Texture2D PipeUpRight    { get; private set; }



        /* Enemy Textures */
        public static Texture2D GoombaWalking     { get; private set; }
        public static Texture2D GoombaDead        { get; private set; }
        public static Texture2D KoopaWalkingLeft  { get; private set; }
        public static Texture2D KoopaWalkingRight { get; private set; }



        /* Projectile Textures */
        public static Texture2D ShellGreenIdle     { get; private set; }
        public static Texture2D ShellGreenSpinning { get; private set; }
        public static Texture2D ProjectileFireball { get; private set; }
        public static Texture2D ProjectileTNT      { get; private set; }



        /* Particle Textures */
        public static Texture2D ParticleBrokenBlock { get; private set; }



        /* Collectable Textures */
        public static Texture2D GreenMushroom { get; private set; }
        public static Texture2D Coin          { get; private set; }
        public static Texture2D FireFlower    { get; private set; }
        public static Texture2D PowerupTNT    { get; private set; }
        public static Texture2D PowerupGun    { get; private set; }
        public static Texture2D RedMushroom   { get; private set; }
        public static Texture2D Star          { get; private set; }

   

        /* Misc. Textures */
        public static Texture2D BackgroundSmall { get; private set; }
        public static Texture2D Background { get; private set; }
        public static Texture2D Outline    { get; private set; }
        public static Texture2D Flag       { get; private set; }
        public static Texture2D Flagpole   { get; private set; }



        public static void LoadAllTextures(Game game)
        {
            /* Mario */
            MarioDead               = game.Content.Load<Texture2D>("Mario/deadMario");
            // Small Mario
            MarioSmallIdleRight     = game.Content.Load<Texture2D>("Mario/Small/rightMarioIdle");
            MarioSmallJumpingRight  = game.Content.Load<Texture2D>("Mario/Small/rightMarioJumping");
            MarioSmallRunningRight  = game.Content.Load<Texture2D>("Mario/Small/rightMarioRunning");
            MarioSmallIdleLeft      = game.Content.Load<Texture2D>("Mario/Small/leftMarioIdle");
            MarioSmallJumpingLeft   = game.Content.Load<Texture2D>("Mario/Small/leftMarioJumping");
            MarioSmallRunningLeft   = game.Content.Load<Texture2D>("Mario/Small/leftMarioRunning");
            // Big Mario
            MarioBigIdleRight       = game.Content.Load<Texture2D>("Mario/Big/rightMarioIdle");
            MarioBigJumpingRight    = game.Content.Load<Texture2D>("Mario/Big/rightMarioJumping");
            MarioBigRunningRight    = game.Content.Load<Texture2D>("Mario/Big/rightMarioRunning");
            MarioBigCrouchingRight  = game.Content.Load<Texture2D>("Mario/Big/rightMarioCrouching");
            MarioBigIdleLeft        = game.Content.Load<Texture2D>("Mario/Big/leftMarioIdle");
            MarioBigJumpingLeft     = game.Content.Load<Texture2D>("Mario/Big/leftMarioJumping");
            MarioBigRunningLeft     = game.Content.Load<Texture2D>("Mario/Big/leftMarioRunning");
            MarioBigCrouchingLeft   = game.Content.Load<Texture2D>("Mario/Big/leftMarioCrouching");
            // Fire Mario
            MarioFireIdleRight      = game.Content.Load<Texture2D>("Mario/Fire/rightMarioIdle");
            MarioFireJumpingRight   = game.Content.Load<Texture2D>("Mario/Fire/rightMarioJumping");
            MarioFireRunningRight   = game.Content.Load<Texture2D>("Mario/Fire/rightMarioRunning");
            MarioFireCrouchingRight = game.Content.Load<Texture2D>("Mario/Fire/rightMarioCrouching");
            MarioFireIdleLeft       = game.Content.Load<Texture2D>("Mario/Fire/leftMarioIdle");
            MarioFireJumpingLeft    = game.Content.Load<Texture2D>("Mario/Fire/leftMarioJumping");
            MarioFireRunningLeft    = game.Content.Load<Texture2D>("Mario/Fire/leftMarioRunning");
            MarioFireCrouchingLeft  = game.Content.Load<Texture2D>("Mario/Fire/leftMarioCrouching");
            // TNT Mario
            MarioTNTIdleRight =      game.Content.Load<Texture2D>("Mario/TNT/rightMarioIdle");
            MarioTNTJumpingRight =   game.Content.Load<Texture2D>("Mario/TNT/rightMarioJumping");
            MarioTNTRunningRight =   game.Content.Load<Texture2D>("Mario/TNT/rightMarioRunning");
            MarioTNTCrouchingRight = game.Content.Load<Texture2D>("Mario/TNT/rightMarioCrouching");
            MarioTNTIdleLeft =       game.Content.Load<Texture2D>("Mario/TNT/leftMarioIdle");
            MarioTNTJumpingLeft =    game.Content.Load<Texture2D>("Mario/TNT/leftMarioJumping");
            MarioTNTRunningLeft =    game.Content.Load<Texture2D>("Mario/TNT/leftMarioRunning");
            MarioTNTCrouchingLeft =  game.Content.Load<Texture2D>("Mario/TNT/leftMarioCrouching");
            // Gun Mario
            MarioGunIdleRight      = game.Content.Load<Texture2D>("Mario/Gun/rightMarioIdle");
            MarioGunJumpingRight   = game.Content.Load<Texture2D>("Mario/Gun/rightMarioJumping");
            MarioGunRunningRight   = game.Content.Load<Texture2D>("Mario/Gun/rightMarioRunning");
            MarioGunCrouchingRight = game.Content.Load<Texture2D>("Mario/Gun/rightMarioCrouching");
            MarioGunIdleLeft       = game.Content.Load<Texture2D>("Mario/Gun/leftMarioIdle");
            MarioGunJumpingLeft    = game.Content.Load<Texture2D>("Mario/Gun/leftMarioJumping");
            MarioGunRunningLeft    = game.Content.Load<Texture2D>("Mario/Gun/leftMarioRunning");
            MarioGunCrouchingLeft  = game.Content.Load<Texture2D>("Mario/Gun/leftMarioCrouching");

            /* Enemies */
            GoombaWalking     = game.Content.Load<Texture2D>("Goomba/goombaWalking");
            GoombaDead        = game.Content.Load<Texture2D>("Goomba/goombaDead");
            KoopaWalkingLeft  = game.Content.Load<Texture2D>("Koopa/koopaTroopaWalkingLeft");
            KoopaWalkingRight = game.Content.Load<Texture2D>("Koopa/koopaTroopaWalkingRight");

            /* Projectiles */
            ShellGreenIdle     = game.Content.Load<Texture2D>("Koopa/shellIdle");
            ShellGreenSpinning = game.Content.Load<Texture2D>("Koopa/shellSpinning");
            ProjectileFireball = game.Content.Load<Texture2D>("Projectiles/fireball");
            ProjectileTNT      = game.Content.Load<Texture2D>("Projectiles/tnt");

            /* Particles */
            ParticleBrokenBlock = game.Content.Load<Texture2D>("Particles/brokenBlock");

            /* Collectables */
            GreenMushroom = game.Content.Load<Texture2D>("Items/oneUp");
            Coin          = game.Content.Load<Texture2D>("Items/coin");
            FireFlower    = game.Content.Load<Texture2D>("Items/fireflower");
            PowerupTNT    = game.Content.Load<Texture2D>("Items/tnt");
            PowerupGun    = game.Content.Load<Texture2D>("Items/gun");
            RedMushroom   = game.Content.Load<Texture2D>("Items/mushroom");
            Star          = game.Content.Load<Texture2D>("Items/star");

            /* Blocks */
            BlockBrick    = game.Content.Load<Texture2D>("Blocks/brickBlock");
            BlockPlatform = game.Content.Load<Texture2D>("Blocks/platformBlock");
            BlockCracked  = game.Content.Load<Texture2D>("Blocks/crackedBlock");
            BlockMystery  = game.Content.Load<Texture2D>("Blocks/questionMarkBlock");
            BlockUsed     = game.Content.Load<Texture2D>("Blocks/usedBlock");

            /* Pipes */
            PipeLeft        = game.Content.Load<Texture2D>("Pipes/pipeLeft");
            PipeRight       = game.Content.Load<Texture2D>("Pipes/pipeRight");
            PipeSide        = game.Content.Load<Texture2D>("Pipes/pipeSide");
            PipeLeftTop     = game.Content.Load<Texture2D>("Pipes/pipeLeftTop");
            PipeLeftBottom  = game.Content.Load<Texture2D>("Pipes/pipeLeftBottom");
            PipeRightTop    = game.Content.Load<Texture2D>("Pipes/pipeRightTop");
            PipeRightBottom = game.Content.Load<Texture2D>("Pipes/pipeRightBottom");
            PipeSideTop     = game.Content.Load<Texture2D>("Pipes/pipeSideTop");
            PipeSideBottom  = game.Content.Load<Texture2D>("Pipes/pipeSideBottom");
            
            PipeTop        = game.Content.Load<Texture2D>("Pipes/pipeTop");
            PipeUpLeftTop  = game.Content.Load<Texture2D>("Pipes/pipeUpLeftTop");
            PipeUpRightTop = game.Content.Load<Texture2D>("Pipes/pipeUpRightTop");
            PipeUp         = game.Content.Load<Texture2D>("Pipes/pipeUp");
            PipeUpLeft     = game.Content.Load<Texture2D>("Pipes/pipeUpLeft");
            PipeUpRight    = game.Content.Load<Texture2D>("Pipes/pipeUpRight");

            /* Misc. */
            BackgroundSmall = game.Content.Load<Texture2D>("Background/background");
            Background = game.Content.Load<Texture2D>("Background/background3");
            Outline    = game.Content.Load<Texture2D>("outline");
            Flag       = game.Content.Load<Texture2D>("Flag/flag");
            Flagpole   = game.Content.Load<Texture2D>("Flag/flagpole");
        }
    }
}