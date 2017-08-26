using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static SuperMarioYeah.Collision.Direction;

namespace SuperMarioYeah.Collision
{
    public interface IPolygon
    {
        float Height { get; }
        float Width { get; }

        Direction DoesIntersect(IPolygon p);
        Vector2 Overlap(IPolygon p);
    }
}
