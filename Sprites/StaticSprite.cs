using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioYeah.Sprites
{
    public class StaticSprite : AbstractSprite
    {
        public override Point Bounds { get { return srcRectangle.Size; } }

        private Rectangle srcRectangle;

        public StaticSprite(Texture2D texture, int rows = 1, int cols = 1, int xindex = 0, int yindex = 0, bool show = true)
            : this(texture, new Rectangle((texture.Width / cols) * xindex, (texture.Height / rows) * yindex, texture.Width / cols, texture.Height / rows), show)
        {

        }

        public StaticSprite(Texture2D texture, Rectangle srcRectangle, bool show = true) : base(texture, show)
        {
            this.srcRectangle = srcRectangle;
        }

        public override void Update() { }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, float scale, bool chain)
        {
            Rectangle destRectangle = new Rectangle(location.ToPoint(), new Point((int)(Bounds.X * scale), (int)(Bounds.Y * scale)));
            Draw(spriteBatch, destRectangle, color, chain);
        }
        public override void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color, bool chain)
        {
            if(Visible)
            {
                if(!chain) { spriteBatch.Begin(); }
                spriteBatch.Draw(texture, destination, srcRectangle, color);
                if (!chain) { spriteBatch.End(); }
            }
        }
    }
}