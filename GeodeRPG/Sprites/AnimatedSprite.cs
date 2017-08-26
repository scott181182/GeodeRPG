using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioYeah.Sprites
{
    public class AnimatedSprite : AbstractSprite
    {
        private int rows, cols;
        public bool Wraparound { get; set; }

        public override Point Bounds { get { return new Point(texture.Width / cols, texture.Height / rows); } }

        private int currentFrame, startFrame, endFrame, frameTime, frameCounter;

        public AnimatedSprite(Texture2D texture, int rows = 1, int cols = 1, int index = 0, int endFrame = -1, int frameTime = 1, bool wraparound = false, bool show = true) : base(texture, show)
        {
            this.rows = rows;
            this.cols = cols;
            this.currentFrame = 0;
            this.startFrame = index;
            this.endFrame = endFrame < 0 ? rows * cols : endFrame + 1;
            this.frameCounter = 0;
            this.frameTime = frameTime;
            this.Wraparound = wraparound;
        }

        public override void Update()
        {
            if (!Visible || startFrame == endFrame) { return; }

            frameCounter++;
            if (frameCounter < frameTime) { return; }
            frameCounter = 0;

            int delta = endFrame > startFrame ? 1 : -1;
            if (Wraparound && currentFrame + delta == endFrame)
            {
                int save = startFrame;
                startFrame = endFrame - delta;
                endFrame = save - delta;
                delta = endFrame > startFrame ? 1 : -1;
            }

            currentFrame += delta;
            if (currentFrame == endFrame) { currentFrame = startFrame; }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, float scale = 1, bool chain = false)
        {
            if (!Visible) { return; }

            int width = texture.Width / cols;
            int height = texture.Height / rows;

            Rectangle destRectangle = new Rectangle((int)location.X, (int)location.Y,
                (int)(width * scale),
                (int)(height * scale));

            Draw(spriteBatch, destRectangle, color, chain);
        }
        public override void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color, bool chain)
        {
            if(!Visible) { return; }

            int width = texture.Width / cols;
            int height = texture.Height / rows;
            int col = currentFrame % cols;
            int row = (currentFrame - col) / cols;

            Rectangle srcRectangle = new Rectangle(width * col, height * row, width, height);

            if (!chain) { spriteBatch.Begin(); }
            spriteBatch.Draw(texture, destination, srcRectangle, color);
            if (!chain) { spriteBatch.End(); }
        }
    }
}