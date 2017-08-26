using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioYeah.Sprites
{
    public interface ISprite : IScreenDrawable
    {
        void Update();
        Point Bounds { get; }
    }
}
