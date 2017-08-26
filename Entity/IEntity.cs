using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Collision;

namespace SuperMarioYeah.Entities
{
    public interface IEntity : IDrawable, Util.ISaveable<IEntity>
    {
        bool IsAlive { get; }
        void Kill();

        void Update();
        void Draw(SpriteBatch spritebatch, float scale, bool chain);

        void Move(float x, float y);
        void Accelerate(float x, float y);

        float Width { get; }
        float Height { get; }
        Vector2 Velocity { get; set; }
        Vector2 Position { get; set; }

        bool IsGravitable { get; set; }
        bool IsGrounded { get; set; }
        bool IsTransitioning { get; }
    }
}