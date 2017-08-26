using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioYeah
{
    public interface IDrawable
    {
        bool Visible { get; set; }

        void Draw(SpriteBatch spriteBatch, Vector2 location, float scale, bool chain);
        void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, float scale, bool chain);
    }
}