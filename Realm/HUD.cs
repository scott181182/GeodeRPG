using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioYeah.Realm
{
    public class HUD
    {
        private Vector2 marioPos = new Vector2(20, 10);
        private Vector2 pointsPos = new Vector2(20, 40);
        private Vector2 coinPos = new Vector2(254, 54);
        private Vector2 coinsPos = new Vector2(280, 40);
        private Vector2 worldPos = new Vector2(460, 10);
        private Vector2 levelPos = new Vector2(480, 40);
        private Vector2 timePos = new Vector2(640, 10);
        private Vector2 timeLeftPos = new Vector2(640, 40);
        private Vector2 pausedPos = new Vector2(320, 240);

        public HUD() {  }

        

        public void Draw(SpriteBatch spriteBatch, int points, int coins, int zone, int level, int time, WorldState state)
        {
            spriteBatch.Begin();
            
            spriteBatch.DrawString(Fonts.CourierNew, "Mario", marioPos, Color.White);
            String strPoints = points.ToString().PadLeft(6, '0');
            spriteBatch.DrawString(Fonts.CourierNew, strPoints, pointsPos, Color.White);

            StaticSprite coin = new StaticSprite(Textures.Coin);
            coin.Draw(spriteBatch, coinPos, Color.White, 1.5f, true);

            String strCoins = "x" + coins.ToString().PadLeft(2, '0');
            spriteBatch.DrawString(Fonts.CourierNew, strCoins, coinsPos, Color.White);

            spriteBatch.DrawString(Fonts.CourierNew, "World", worldPos, Color.White);
            String strLevel = zone.ToString() + "-" + level.ToString();
            spriteBatch.DrawString(Fonts.CourierNew, strLevel, levelPos, Color.White);

            spriteBatch.DrawString(Fonts.CourierNew, "Time", timePos, Color.White);
            String strTimeLeft = (time / 30).ToString().PadLeft(3, '0');
            spriteBatch.DrawString(Fonts.CourierNew, strTimeLeft, timeLeftPos, Color.White);

            if (state == WorldState.PAUSED)
            {
                spriteBatch.DrawString(Fonts.CourierNew, "Paused", pausedPos, Color.White);
            }
            spriteBatch.End();
        }

    }
        
}
