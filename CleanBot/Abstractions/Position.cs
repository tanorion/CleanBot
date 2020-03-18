using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions
{
    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveX(int value)
        {
            X += value;
        }
        public void MoveY(int value)
        {
            Y += value;
        }
        public int X { get; private set; }
        public int Y { get; private set; }
    }
}
