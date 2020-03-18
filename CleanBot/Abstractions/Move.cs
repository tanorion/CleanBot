using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions
{
    public class Move
    {
        public Move(MoveDirection direction, int distance)
        {
            Direction = direction;
            Distance = distance;

        }
        public MoveDirection Direction { get; }
        public int Distance { get; }
    }

    public enum MoveDirection
    {
        E,
        W,
        S,
        N
    }
}
