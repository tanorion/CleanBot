using System;
using Abstractions;

public class LineSegment : IEquatable<LineSegment>
{
    public Position Start { get; set; }
    public Position End { get; set; }
    public bool Vertical { get; set; }
    public int StaticPositionValue { get; set; }

    public bool Equals(LineSegment other)
    {
        return (other.Start.X == Start.X && other.Start.Y == Start.Y && other.End.X == End.X &&
                other.End.Y == End.Y) ||
               (other.Start.X == End.X && other.Start.Y == End.Y && other.End.X == Start.X &&
                other.End.Y == Start.Y);
    }

    public static bool TryCombineSegment(LineSegment p, LineSegment q, out LineSegment r)
    {
        if (p.Vertical != q.Vertical || p.StaticPositionValue != q.StaticPositionValue)
        {
            r = null;
            return false;
        }

        int startValue;
        int endValue;
        if (!p.Vertical)
        {
            if (p.Start.X >= q.Start.X && p.Start.X <= q.End.X)
            {
                startValue = q.Start.X;
            }
            else if (q.Start.X >= p.Start.X && q.Start.X <= p.End.X)
            {
                startValue = p.Start.X;
            }
            else
            {
                r = null;
                return false;
            }

            if (p.End.X >= q.Start.X && p.End.X <= q.End.X)
            {
                endValue = q.End.X;
            }
            else if (q.End.X >= p.Start.X && q.End.X <= p.End.X)
            {
                endValue = p.Start.X;
            }
            else
            {
                r = null;
                return false;
            }

            r= new LineSegment()
            {
                Start = new Position(startValue,p.StaticPositionValue),
                End = new Position(endValue,p.StaticPositionValue),
                StaticPositionValue = p.StaticPositionValue,
                Vertical = p.Vertical
            };
            return true;
        }

        if (p.Vertical)
        {
            if (p.Start.Y >= q.Start.Y && p.Start.Y <= q.End.Y)
            {
                startValue = q.Start.Y;
            }
            else if (q.Start.Y >= p.Start.Y && q.Start.Y <= p.End.Y)
            {
                startValue = p.Start.Y;
            }
            else
            {
                r = null;
                return false;
            }

            if (p.End.Y >= q.Start.Y && p.End.Y <= q.End.Y)
            {
                endValue = q.End.Y;
            }
            else if (q.End.Y >= p.Start.Y && q.End.Y <= p.End.Y)
            {
                endValue = p.Start.Y;
            }
            else
            {
                r = null;
                return false;
            }

            r = new LineSegment()
            {
                Start = new Position(p.StaticPositionValue, startValue),
                End = new Position(p.StaticPositionValue, endValue),
                StaticPositionValue = p.StaticPositionValue,
                Vertical = p.Vertical
            };
            return true;
        }

        r = null;
        return false;
    }

    public static bool Intersect(LineSegment p, LineSegment q, out Position r)
    {
        if (p.Vertical == q.Vertical )
        {
            r = null;
            return false;
        }

        if (p.Vertical)
        {
            if (p.StaticPositionValue >= q.Start.X && p.StaticPositionValue <= q.End.X &&
                q.StaticPositionValue >= p.Start.Y && q.StaticPositionValue <= p.End.Y)
            {
                r=new Position(q.StaticPositionValue,p.StaticPositionValue);
                return true;
            }
            
        }

        if (q.Vertical)
        {
            if (q.StaticPositionValue >= p.Start.X && q.StaticPositionValue <= p.End.X &&
                p.StaticPositionValue >= q.Start.Y && p.StaticPositionValue <= q.End.Y)
            {
                r = new Position(p.StaticPositionValue, q.StaticPositionValue);
                return true;
            }

        }
        r = null;
        return false;
    }
}