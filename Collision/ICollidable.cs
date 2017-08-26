using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Realm;
using static SuperMarioYeah.Collision.Direction;

namespace SuperMarioYeah.Collision
{
    public interface ICollidable
    {
        void OnCollision(ICollidable obj, Direction dir, World world);
        IPolygon BoundingPolygon { get; }
    }
}
