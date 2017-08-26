using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Collision;

namespace SuperMarioYeah.Realm.Blocks
{
    public class StarBrickBlock : Block
    {
        public StarBrickBlock(int id) : base(id, new Sprites.StaticSprite(Textures.BlockBrick))
        {

        }

        public override void OnCollision(int x, int y, ICollidable obj, Direction dir, World world)
        {
            base.OnCollision(x, y, obj, dir, world);

            if(dir == Direction.Down && obj is Entities.Mario)
            {
                world.AddEntity(new Collectable.Star(new Vector2(x, y + 1)));
                world.SetTileAt(x, y, Block.usedBlock);
            }
        }
    }
}
