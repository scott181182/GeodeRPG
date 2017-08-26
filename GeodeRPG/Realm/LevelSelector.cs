using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioYeah.Realm
{
    public class LevelSelector
    {
        public string[] Levels = {
            "..\\..\\..\\..\\1 - 1.csv",
            "..\\..\\..\\..\\1 - 1 Modified.csv",
            "..\\..\\..\\..\\TNT.csv"
        };
        public string[] LevelNames = {
            "1-1",
            "1-1 Modified",
            "TNT"
        };

        private int selectedLevel;
        public string CurrentLevel { get => Levels[selectedLevel]; }

        private MarioGame game;
        
        private Vector2 titlePos = new Vector2(150, 40);

        public LevelSelector(MarioGame game)
        {
            this.game = game;
            selectedLevel = 0;
        }

        public void ChangeSelection(int move)
        {
            selectedLevel = (selectedLevel + move) % Levels.Length;
            if(selectedLevel < 0) { selectedLevel += Levels.Length; }
        }

        public void SelectLevel()
        {
            game.GameWorld = WorldLoader.LoadWorld(Levels[selectedLevel], game);
            game.State = GameState.InGame;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Textures.BackgroundSmall, Camera.VIEWPORT, Color.White);

            spriteBatch.DrawString(Fonts.CourierNew, "Super Mario YEAH!", titlePos, Color.White);

            Vector2 currentPos = new Vector2();
            for (int i = 0; i < LevelNames.Length; i++)
            {
                currentPos = new Vector2(100, (1 + i) * (Camera.CAMERA_HEIGHT / (LevelNames.Length + 1)));
                spriteBatch.DrawString(Fonts.CourierNew, LevelNames[i], currentPos, Color.White);
            }

            spriteBatch.DrawString(Fonts.CourierNew, "->", new Vector2(30, (1 + selectedLevel) * (Camera.CAMERA_HEIGHT / (LevelNames.Length + 1))), Color.White);

            spriteBatch.End();
        }
    }
}
