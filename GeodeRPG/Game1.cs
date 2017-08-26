using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Input;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Realm;
using System.Threading;
using SuperMarioYeah.Sounds;
namespace SuperMarioYeah
{
    public enum GameState { InGame, WorldSelector }
    public class MarioGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;


        public Action Quit, LevelReset, MenuReset;
        public World GameWorld;
        public LevelSelector Selector;
        public GameState State { get; set; }
        public int level;

        public Boolean testing = false;

        private List<IController> controllers;

        public MarioGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Camera.CAMERA_WIDTH,
                PreferredBackBufferHeight = Camera.CAMERA_HEIGHT
            };

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Quit = () => { this.Exit(); };
            LevelReset = () => { if (State == GameState.InGame) GameWorld = WorldLoader.LoadWorld(Selector.CurrentLevel, this); };
            MenuReset = () => { State = GameState.WorldSelector; };

            InputActions.LoadInputActions(this);
            controllers = new List<IController>();
            KeyboardController keyboard = new KeyboardController(this);
            GamePadController gamepad = new GamePadController(this);
            controllers.Add(keyboard);
            controllers.Add(gamepad);

            Selector = new LevelSelector(this);
            State = GameState.WorldSelector;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.LoadAllTextures(this);
            SoundLoader.LoadSounds(this);
            Fonts.LoadFonts(this);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers) { controller.Update(); }

            if (State == GameState.InGame) { GameWorld.Update(); }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(93, 148, 251));

            switch (State)
            {
                case GameState.WorldSelector: Selector.Draw(spriteBatch); break;
                case GameState.InGame: GameWorld.Draw(spriteBatch); break;
            }

            base.Draw(gameTime);
        }
    }
}
