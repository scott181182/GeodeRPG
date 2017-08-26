using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioYeah
{
    public static class Fonts
    {
        public static SpriteFont CourierNew {get; private set;}

        public static void LoadFonts(Game game)
        {

            CourierNew = game.Content.Load<SpriteFont>("Fonts/Courier");
        }
    }
}
