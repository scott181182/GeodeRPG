using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Realm;

namespace SuperMarioYeah.Sprites
{
    public abstract class AbstractSprite : ISprite
    {
        public bool Visible { get; set; }
        public virtual Point Bounds { get { return new Point(texture.Width, texture.Height); } }

        protected Texture2D texture;

        protected AbstractSprite(Texture2D texture, bool show = true)
        {
            this.texture = texture;
            this.Visible = show;
        }

        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, float scaleOffset, bool chain);
        public void Draw(SpriteBatch spriteBatch, Vector2 location, float scale, bool chain)
        {
            Draw(spriteBatch, location, Color.White, scale, chain);
        }
        public abstract void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color, bool chain);
        public void Draw(SpriteBatch spriteBatch, Rectangle destination, bool chain)
        {
            Draw(spriteBatch, destination, Color.White, chain);
        }
    }
}