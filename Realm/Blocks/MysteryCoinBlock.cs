using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Particles;
namespace SuperMarioYeah.Realm.Blocks
{
    public class MysteryCoinBlock : Block
    {
        public MysteryCoinBlock(int id) : base(id, new AnimatedSprite(Textures.BlockMystery, rows: 4, cols: 1, frameTime: 8)) {  }

        public override void OnCollision(int x, int y, ICollidable obj, Direction dir, World world)
        {
            base.OnCollision(x, y, obj, dir, world);

            if (dir == Direction.Down && obj is Entities.Mario)
            {
                
                world.AddEntity(new CoinParticle(x,y));
                world.CollectCoin(new Collectable.Coin(new Vector2(x, y + 1)));
                world.SetTileAt(x, y, Block.usedBlock);
            }
        }
    }


}
