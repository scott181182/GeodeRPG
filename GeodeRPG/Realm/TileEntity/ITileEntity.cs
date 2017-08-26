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

namespace SuperMarioYeah.Realm
{
    public interface ITileEntity : IEntity
    {
        Block GetBlock();
    }
}
