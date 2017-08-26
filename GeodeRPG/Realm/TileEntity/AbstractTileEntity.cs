using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Realm.TileEntity
{
    public abstract class AbstractTileEntity : ITileEntity
    {
        public bool Visible { get { return false; } set { } }
        public bool IsGravitable { get { return false; } set { } }
        public bool IsGrounded { get { return true; } set { } }
        public bool IsTransitioning { get { return false; } }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get { return Vector2.Zero; } set { } }

        public virtual float Width { get { return 1; } }
        public virtual float Height { get { return 1; } }
        public IPolygon BoundingPolygon
        {
            get { return new Collision.BoundingBox(Position.X, Position.Y, 1, 1); }
        }

        private bool alive = true;
        public bool IsAlive { get => alive; }
        public void Kill() { alive = false; }


        private Block block;

        public AbstractTileEntity(Block block, int x, int y)
        {
            this.block = block;
            Position = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, float scale, bool chain) {  }
        public void Draw(SpriteBatch spritebatch, float scale, bool chain) {  }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, float scale, bool chain) {  }
        public void Draw(SpriteBatch spriteBatch, Rectangle destination, bool chain) {  }
        public void Move(float x, float y) {  }
        public void Accelerate(float x, float y) { }

        public void OnCollision(ICollidable obj, Direction dir, World world) {  }

        public virtual void Update() {  }
        public Block GetBlock() { return block; }
        public abstract IEntity Clone();
    }
}
