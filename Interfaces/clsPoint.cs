using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace General
{
    public class clsPoint : IComparable<clsPoint> ,IEqualityComparer<clsPoint>
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
            return Math.Sqrt(Math.Pow((b.Y - a.Y),2)  + Math.Pow((b.X - a.X), 2));
        }

        public double Angle(clsPoint target)
        {
            return Math.Atan2(target.X - X, target.Y - Y);
        }
        public double AnglePos(clsPoint target)
        {
            double angle = Math.Atan2(target.X - X, target.Y - Y);
            if (angle>=0)
            {
                return angle;
            }
            return angle+2*Math.PI;
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

        public IEnumerable<clsPoint> Move(string instruction)
        {
            return Move(instruction[0], int.Parse(instruction.Substring(1)));
        }
        public IEnumerable<clsPoint> Move(char Direction, int Distance)
        {
            List<clsPoint> points = new List<clsPoint>();
            for (int i = 1; i <= Distance; i++)
            {
                switch (Direction)
                {
                    case 'R':
                        points.Add(plus(i, 0));
                        break;
                    case 'L':
                        points.Add(plus(-i, 0));
                        break;
                    case 'U':
                        points.Add(plus(0, i));
                        break;
                    case 'D':
                        points.Add(plus(0, -i));
                        break;
                }
            }
            
            return points;
        }

        public bool Equals([AllowNull] clsPoint x, [AllowNull] clsPoint y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode([DisallowNull] clsPoint obj)
        {
            return obj.X + obj.Y;
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            if (obj==null)
            {
                return false;
            }
            else if(obj is clsPoint)
            {
                return Equals(this, (clsPoint)obj);
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return X+","+Y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
