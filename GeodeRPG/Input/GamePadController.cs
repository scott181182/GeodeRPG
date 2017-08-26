using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SuperMarioYeah;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Input
{
    public class GamePadController : IController
    {
        private MarioGame game;
        private GamePadState previous;

        private Dictionary<Buttons, Action> downActions;
        private Dictionary<Buttons, Action> pressActions;
        private Dictionary<Buttons, Action> releaseActions;

        public GamePadController(MarioGame game)
        {
            this.game = game;
            previous = GamePad.GetState(PlayerIndex.One);

            downActions = new Dictionary<Buttons, Action>();
            pressActions = new Dictionary<Buttons, Action>();
            releaseActions = new Dictionary<Buttons, Action>();

            this.OnButtonPressed(Buttons.Start, game.Quit);

            this.OnButtonPressRelease(Buttons.DPadUp,   ActionUtil.Chain(InputActions.jump, InputActions.menuUp), InputActions.halt);
            this.OnButtonDownRelease(Buttons.DPadDown, InputActions.crouch, InputActions.halt);
            this.OnButtonDownRelease(Buttons.DPadLeft, InputActions.moveLeft, InputActions.halt);
            this.OnButtonDownRelease(Buttons.DPadRight, InputActions.moveRight, InputActions.halt);

            this.OnButtonPressed(Buttons.A, InputActions.fire);
            this.OnButtonPressed(Buttons.DPadDown, InputActions.menuDown);
        }

        private void OnButtonDown(Buttons button, Action act) { downActions.Add(button, act); }
        private void OnButtonPressed(Buttons button, Action act) { pressActions.Add(button, act); }
        private void OnButtonReleased(Buttons button, Action act) { releaseActions.Add(button, act); }
        private void OnButtonDownRelease(Buttons button, Action down, Action release)
        {
            downActions.Add(button, down);
            releaseActions.Add(button, release);
        }
        private void OnButtonPressRelease(Buttons button, Action press, Action release)
        {
            pressActions.Add(button, press);
            releaseActions.Add(button, release);
        }

        public void Update()
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);

            PollDownActions(state);
            PollPressActions(state);
            PollReleaseActions(state);

            previous = state;
        }

        private void PollDownActions(GamePadState current)
        {
            foreach (KeyValuePair<Buttons, Action> pair in downActions)
            {
                if (current.IsButtonDown(pair.Key))
                {
                    downActions[pair.Key]();
                }
            }
        }
        private void PollPressActions(GamePadState current)
        {
            foreach (KeyValuePair<Buttons, Action> pair in pressActions)
            {
                if (current.IsButtonDown(pair.Key) && !previous.IsButtonDown(pair.Key))
                {
                    pressActions[pair.Key]();
                }
            }
        }
        private void PollReleaseActions(GamePadState current)
        {
            foreach (KeyValuePair<Buttons, Action> pair in releaseActions)
            {
                if (!current.IsButtonDown(pair.Key) && previous.IsButtonDown(pair.Key))
                {
                    releaseActions[pair.Key]();
                }
            }
        }
    }
}