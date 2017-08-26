using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioYeah.Collision
{
    public enum Direction {
        Up,
        Down,
        Left,
        Right,
        None,
        Unknown
    }
    public static class Dir
    {
        public static Direction Reverse(this Direction dir)
        {
            switch(dir)
            {
                case Direction.Up: return Direction.Down;
                case Direction.Down: return Direction.Up;
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
                default: return dir;
            }
        }
        public static bool IsHorizontal(this Direction dir) { return dir == Direction.Left || dir == Direction.Right; }
        public static bool IsVertical(this Direction dir) { return dir == Direction.Up || dir == Direction.Down; }
    }
}
