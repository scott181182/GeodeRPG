using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperMarioYeah;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm;

namespace SuperMarioYeah.Input
{
    public class KeyboardController : IController
    {
        private MarioGame game;
        private KeyboardState previous;

        private Dictionary<Keys, Action> downActions;
        private Dictionary<Keys, Action> pressActions;
        private Dictionary<Keys, Action> releaseActions;

        public KeyboardController(MarioGame game)
        {
            this.game = game;

            downActions = new Dictionary<Keys, Action>();
            pressActions = new Dictionary<Keys, Action>();
            releaseActions = new Dictionary<Keys, Action>();

            previous = Keyboard.GetState();

            this.OnKeyPressed(Keys.Q, game.Exit);
            this.OnKeyPressed(Keys.R, game.LevelReset);
            this.OnKeyPressed(Keys.M, game.MenuReset);
            this.OnKeyPressed(Keys.Enter, InputActions.start);

            this.OnKeyPressed(Keys.Up,    ActionUtil.Chain(InputActions.jump, InputActions.menuUp));
            this.OnKeyPressed(Keys.W,     ActionUtil.Chain(InputActions.jump, InputActions.menuUp));
            this.OnKeyPressed(Keys.Space, ActionUtil.Chain(InputActions.jump, InputActions.menuUp));

            this.OnKeyDownRelease(Keys.Left, InputActions.moveLeft, InputActions.halt);
            this.OnKeyDownRelease(Keys.A, InputActions.moveLeft, InputActions.halt);
            this.OnKeyDownRelease(Keys.Right, InputActions.moveRight, InputActions.halt);
            this.OnKeyDownRelease(Keys.D, InputActions.moveRight, InputActions.halt);

            this.OnKeyPressed(Keys.F, InputActions.fire);

            this.OnKeyDown(Keys.Down, InputActions.crouch);
            this.OnKeyDown(Keys.S, InputActions.crouch);


            this.OnKeyPressed(Keys.Down, InputActions.menuDown);
            this.OnKeyPressed(Keys.S,    InputActions.menuDown);
        }

        private void OnKeyDown(Keys key, Action act) { downActions.Add(key, act); }
        private void OnKeyPressed(Keys key, Action act) { pressActions.Add(key, act); }
        private void OnKeyReleased(Keys key, Action act) { releaseActions.Add(key, act); }
        private void OnKeyDownRelease(Keys key, Action down, Action release)
        {
            OnKeyDown(key, down);
            OnKeyReleased(key, release);
        }
        private void OnKeyPressRelease(Keys key, Action press, Action release)
        {
            OnKeyPressed(key, press);
            OnKeyReleased(key, release);
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            PollDownActions(state);
            PollPressActions(state);
            PollReleaseActions(state);

            previous = state;
        }

        private void PollDownActions(KeyboardState currState)
        {
            foreach (KeyValuePair<Keys, Action> pair in downActions)
            {
                if (currState.IsKeyDown(pair.Key))
                {
                    downActions[pair.Key]();
                }
            }
        }
        private void PollPressActions(KeyboardState currState)
        {
            foreach (KeyValuePair<Keys, Action> pair in pressActions)
            {
                if (currState.IsKeyDown(pair.Key) && !previous.IsKeyDown(pair.Key))
                {
                    pressActions[pair.Key]();
                }
            }
        }
        private void PollReleaseActions(KeyboardState currState)
        {
            foreach (KeyValuePair<Keys, Action> pair in releaseActions)
            {
                if (previous.IsKeyDown(pair.Key) && !currState.IsKeyDown(pair.Key))
                {
                    releaseActions[pair.Key]();
                }
            }
        }
    }
}