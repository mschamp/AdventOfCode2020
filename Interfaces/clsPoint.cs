using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Interfaces
{
    class clsPoint : IComparable<clsPoint>
    {
        public clsPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public clsPoint plus(int x, int y)
        {
            return new clsPoint(X + x, Y + y);
        }

        public clsPoint plus(clsPoint other)
        {
            return new clsPoint(X + other.X, Y + other.Y);
        }

        public int CompareTo([AllowNull] clsPoint other)
        {
            if (Y == other.Y)
            {
                return X.CompareTo(other.X);
            }
            else
            {
                return Y.CompareTo(other.Y);
            }
        }

        public int manhattan(clsPoint other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }

        public int manhattan()
        {
            return manhattan(new clsPoint(0, 0));
        }

        public double distance(clsPoint other)
        {
            return distance(this, other);
        }
        public double distance(clsPoint a, clsPoint b)
        {
            return Math.Sqrt((b.Y - a.Y) ^ 2 + (b.X - a.X) ^ 2);
        }

        public double Angle(clsPoint target)
        {
            return Math.Atan2(target.X - X, target.Y - Y);
        }

        public bool InBound( int maxX, int maxY)
        {
            return InBound(0, maxX, 0, maxY);
        }

        public bool InBound(int minX, int maxX, int minY, int maxY)
        {
            return (Enumerable.Range(minX, maxX).Contains(X) && Enumerable.Range(minY, maxY).Contains(Y));
        }

        public clsPoint rotate90()
        {
            return new clsPoint(-Y, X);
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
