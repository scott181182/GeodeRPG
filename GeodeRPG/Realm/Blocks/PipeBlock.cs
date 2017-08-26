using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm.TileEntity;
using SuperMarioYeah.Sprites;

namespace SuperMarioYeah.Realm.Blocks
{
    public class PipeBlock : Block
    {
        public PipeBlock(int id, ISprite sprite) : base(id, sprite) {  }

        public override void OnCollision(int x, int y, ICollidable obj, Direction dir, World world)
        {
            base.OnCollision(x, y, obj, dir, world);
            
            if(obj is Mario && dir != Direction.Up)
            {
                PipeTileEntity te = world.GetTileEntityAt<PipeTileEntity>(x, y);
                if(te != null && dir == te.TransitionDirection) { (obj as Mario).GoThroughPipe(te, dir.Reverse()); }
            }
        }
    }
}
