using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SuperMarioYeah.Collision
{
    public interface ICollisionInstance: IComparable<ICollisionInstance>, IEquatable<ICollisionInstance>
    {
        void DoCollision();
        Vector2 GetOverlap();
        float GetArea();
    }
}
