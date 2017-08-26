using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm.TileEntity;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Particles;

namespace SuperMarioYeah.Realm.Blocks
{
    public class CoinBrickBlock : Block
    {
        public CoinBrickBlock(int id) : base(id, new StaticSprite(Textures.BlockBrick))
        {

        }

        public override void OnCollision(int x, int y, ICollidable obj, Direction dir, World world)
        {
            base.OnCollision(x, y, obj, dir, world);

            if(dir == Direction.Down && obj is Entities.Mario)
            {
                CoinTileEntity tile = world.GetTileEntityAt<CoinTileEntity>(x, y);
                tile.Coins--;
                world.AddEntity(new CoinParticle(x, y));
                world.CollectCoin(new Collectable.Coin(new Vector2(x, y + 0.5f)));
                if (tile.Coins == 0)
                {
                    tile.Kill();
                    world.SetTileAt(x, y, Block.usedBlock);
                }
            }
        }
    }
}
