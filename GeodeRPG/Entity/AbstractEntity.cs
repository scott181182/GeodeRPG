using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMarioYeah.Sprites;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;

namespace SuperMarioYeah.Entities
{
    public abstract class AbstractEntity : IEntity
    {
        private bool alive;
        public virtual bool IsAlive { get => alive; }
        public virtual void Kill() { alive = false; }

        public bool Visible
        {
            set { sprite.Visible = value; }
            get { return sprite.Visible; }
        }

        public virtual bool IsGravitable { get; set; }
        public virtual bool IsGrounded { get; set; }
        public abstract bool IsTransitioning { get; }

        public virtual float Width { get { return (float)sprite.Bounds.X / World.BLOCK_SIZE; } }
        public virtual float Height { get { return (float)sprite.Bounds.Y / World.BLOCK_SIZE; } }
        public virtual Vector2 Velocity { get; set; }
        public virtual Vector2 Position { get; set; }

        protected ISprite sprite;

        protected AbstractEntity(ISprite sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.Position = position;
            this.Velocity = Vector2.Zero;
            alive = true;

            IsGravitable = true;
        }
        protected AbstractEntity(Vector2 position)
        {
            this.Position = position;
            this.Velocity = new Vector2(0, 0);
            alive = true;

            IsGravitable = true;
        }

        public virtual void Update() {
            this.sprite.Update();
            Position += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch, float scale = 1, bool chain = false) { this.Draw(spriteBatch, Vector2.Zero, scale, chain); }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 1, bool chain = false)
        {
            this.sprite.Draw(spriteBatch, position, scale, chain);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float scale = 1, bool chain = false)
        {
            this.sprite.Draw(spriteBatch, position, color, scale, chain);
        }
        public abstract IEntity Clone();

        public void Move(float x, float y) { this.Position += new Vector2(x, y); }
        public void Accelerate(float x, float y) { this.Velocity += new Vector2(x, y); }
    }
}
