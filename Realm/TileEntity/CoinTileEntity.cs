using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Realm.TileEntity
{
    public class CoinTileEntity : AbstractTileEntity
    {
        public int Coins { get; set; }

        public CoinTileEntity(int x, int y, int c) : base(Block.coinBrickBlock, x, y)
        {
            Coins = c;
        }

        public override IEntity Clone() => new CoinTileEntity((int)Position.X, (int)Position.Y, Coins);
    }
}
