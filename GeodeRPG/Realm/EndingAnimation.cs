using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Collectable;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm.Blocks;
using SuperMarioYeah.Realm.TileEntity;

namespace SuperMarioYeah.Realm
{
    public static class EndingAnimation
    {
        private static int coinTimer = 10;
        private static int resetTimer = 100;
        private static bool landedAfterEnd = false;
        private static double marioPos = Double.MaxValue;
        public static void Update(World world, Mario mario, Flag flag, int coinsCollected)
        {
            if (mario.Position.Y > 3)
            {
                mario.Move(0, -0.1f);
                mario.Position = new Vector2(flag.Position.X + 0.4f, mario.Position.Y);
                if (flag.Position.Y > 3) { flag.Move(0, -0.1f); }
                landedAfterEnd = false;
                world.EarnPoints(100);
            }
            else if (flag.Position.Y > 3)
            {
                flag.Move(0, -0.1f);
            }
            else if (flag.Position.Y <= 3 && !landedAfterEnd)
            {
                landedAfterEnd = true;
                mario.Position = flag.Position + new Vector2(1, -1);
                marioPos = mario.Position.X + 6.0f;
            }
            else if (coinsCollected > 0 && coinTimer == 0 && landedAfterEnd)
            {
                world.ConvertCoinIntoPoints();
                Sounds.SoundLoader.Coin(mario.Position);
                coinTimer = 10;
            }
            else if (coinsCollected > 0 && landedAfterEnd)
            {
                coinTimer--;
            }
            else if (mario.Position.X < marioPos && coinsCollected == 0 && landedAfterEnd)
            {
                mario.Move(0.1f, 0);
            }
            else if (coinsCollected == 0 && mario.Position.X >= marioPos && landedAfterEnd && resetTimer > 0)
            {
                mario.Visible = false;
                resetTimer--;
            } else if (resetTimer == 0) { world.ExitLevel(); }
        }
    }
}
