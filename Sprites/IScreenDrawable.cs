using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioYeah.Sprites
{
    public interface IScreenDrawable : IDrawable
    {
        void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color, bool chain);
        void Draw(SpriteBatch spriteBatch, Rectangle destination, bool chain);
    }
}
