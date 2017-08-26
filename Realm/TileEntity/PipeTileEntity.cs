using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Realm.TileEntity
{
    public class PipeTileEntity : AbstractTileEntity
    {
        public Direction TransitionDirection { get; private set; }
        public int TransitionRegion { get; private set; }
        public int TransitionX { get; private set; }

        public int TransitionY { get; private set; }

        public PipeTileEntity(int x, int y, Direction dir, int region, int tx, int ty) : base(GetBlockByDirection(dir), x, y)
        {
            TransitionDirection = dir;
            TransitionRegion = region;
            TransitionX = tx;
            TransitionY = ty;
        }

        private static Block GetBlockByDirection(Direction dir)
        {
            switch(dir)
            {
                case Direction.Up:    return Block.pipeUpLeftTop;
                case Direction.Left:  return Block.pipeLeftTop;
                case Direction.Right: return Block.pipeRightTop;
                default: return null;
            }
        }

        public override IEntity Clone()
            => new PipeTileEntity((int)Position.X, (int)Position.Y, TransitionDirection, TransitionRegion, TransitionX, TransitionY);
    }
}
