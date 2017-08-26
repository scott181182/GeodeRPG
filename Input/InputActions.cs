using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Input
{
    public static class InputActions
    {
        public static Action jump, crouch, moveLeft, moveRight, halt, fire, start, menuUp, menuDown;

        public static void LoadInputActions(MarioGame game)
        {
            jump = () => 
            {
                if (game.State == GameState.InGame) { game.GameWorld.Player.Jump(); }
            };

            crouch = () =>
            {
                if (game.State == GameState.InGame)
                {
                    if (game.GameWorld.Player.State != Mario.MarioState.Dead && game.GameWorld.Player.IsGrounded && game.GameWorld.Player.ActionState == Mario.MarioActionState.Idle)
                    {
                        game.GameWorld.Player.ActionState = Mario.MarioActionState.Crouching;
                    }
                }
                
            };

            moveLeft = () =>
            {
                if (game.State == GameState.InGame && game.GameWorld.Player.State != Mario.MarioState.Dead && game.GameWorld.Player.ActionState != Mario.MarioActionState.Crouching)
                {
                    game.GameWorld.Player.FacingRight = false;
                    game.GameWorld.Player.Velocity = new Vector2(-0.1f, game.GameWorld.Player.Velocity.Y);
                }
            };

            moveRight = () =>
            {
                if (game.State == GameState.InGame && game.GameWorld.Player.State != Mario.MarioState.Dead && game.GameWorld.Player.ActionState != Mario.MarioActionState.Crouching)
                {
                    game.GameWorld.Player.FacingRight = true;
                    game.GameWorld.Player.Velocity = new Vector2(0.1f, game.GameWorld.Player.Velocity.Y);
                }
            };

            halt  = () => { if (game.State == GameState.InGame) game.GameWorld.Player.Velocity = new Vector2(0, game.GameWorld.Player.Velocity.Y); };
            fire  = () => { if (game.State == GameState.InGame) game.GameWorld.Player.UseAbility(); };
            start = () => {
                if (game.State == GameState.InGame) game.GameWorld.TogglePause();
                else { game.Selector.SelectLevel(); }
            };

            menuUp =   () => { if (game.State == GameState.WorldSelector) { game.Selector.ChangeSelection(-1); } };
            menuDown = () => { if (game.State == GameState.WorldSelector) { game.Selector.ChangeSelection(1);  } };
        }
    }
}
