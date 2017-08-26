using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Sprites;

namespace SuperMarioYeah.Realm.Blocks
{
    public class PlatformBlock : Block
    {
        public PlatformBlock(int id) : base(id, new StaticSprite(Textures.BlockPlatform)) {  }
    }
}
