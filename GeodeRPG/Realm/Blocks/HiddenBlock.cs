using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Realm.Blocks
{
    public class HiddenBlock : Block
    {
        public HiddenBlock(int id) : base(id, new StaticSprite(Textures.BlockUsed))
        {
            this.Visible = false;
        }

        public override void OnCollision(int x, int y, ICollidable obj, Direction dir, World world)
        {
            IEntity entity = obj as IEntity;
            if (entity == null) { return; }
            Vector2 overlap = obj.BoundingPolygon.Overlap(new Collision.BoundingBox(x, y, 1, 1));

            if (dir == Direction.Down) {
                entity.Position = new Vector2(entity.Position.X, y - obj.BoundingPolygon.Height);
                entity.Velocity = new Vector2(entity.Velocity.X, 0);
                world.SetTileAt(x, y, Block.usedBlock);
                world.AddEntity(new Collectable.OneUp(new Vector2(x, y + 1)));
            }
        }
    }
}
