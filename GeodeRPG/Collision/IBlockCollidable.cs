using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;

namespace SuperMarioYeah.Collision
{
    public interface IBlockCollidable : ICollidable
    {
        void OnCollision(int x, int y, ICollidable obj, Direction dir, World world);
    }
}
