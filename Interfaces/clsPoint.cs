using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace General
{
	public class clsPoint : IComparable<clsPoint> ,IEqualityComparer<clsPoint>
    {
        public clsPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public clsPoint plus(double x, double y)
        {
            return new clsPoint(X + x, Y + y);
        }

        public clsPoint plus(clsPoint other)
        {
            return this + other;
        }

        public static clsPoint operator +(clsPoint p1, clsPoint p2)
        {
            return new clsPoint(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static clsPoint operator -(clsPoint p1,clsPoint p2)
        {
            return new clsPoint(p1.X-p2.X, p1.Y-p2.Y);
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

        public double manhattan(clsPoint other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }

        public double manhattan()
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
            return ((X >= minX && X <= maxX) && (Y >= minY && Y <= maxY));
        }

        public clsPoint rotate90()
        {
            return new clsPoint(-Y, X);
        }

        public clsPoint rotate(double Angle)
        {
            return new clsPoint(Math.Cos(Angle) * X - Math.Sin(Angle) * Y,
                Math.Sin(Angle) * X + Math.Cos(Angle) * Y);
        }

        public clsPoint rotateDegrees(double Angle)
        {
            return rotate(Angle/180.0*Math.PI);
        }

        public IEnumerable<clsPoint> Move(string instruction)
        {
            return Move(instruction[0], int.Parse(instruction.Substring(1)));
        }
        public IEnumerable<clsPoint> Move(char Direction, int Distance)
        {
            List<clsPoint> points = [];
            for (int i = 1; i <= Distance; i++)
            {
                switch (Direction)
                {
                    case 'R':
                    case 'E':
                    case '>':
                        points.Add(plus(i, 0));
                        break;
                    case 'L':
                    case 'W':
                    case '<':
                        points.Add(plus(-i, 0));
                        break;
                    case 'U':
                    case 'N':
                    case '^':
                        points.Add(plus(0, i));
                        break;
                    case 'D':
                    case 'S':
                    case 'v':
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
            return (int)(obj.X + obj.Y);
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
            else if(obj is clsPoint point)
            {
                return Equals(this, point);
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return X+","+Y;
        }
        public double X { get; set; }
        public double Y { get; set; }

        public clsPoint Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return plus(0, 1);
                case Direction.Left:
                    return plus(-1, 0);
                case Direction.Down:
                    return plus(0, -1);
                case Direction.Right:
                    return plus(1, 0);
            }
            return new clsPoint(0, 0);
        }

        public clsPoint Move(Direction direction, int distance)
        {
            switch (direction)
            {
                case Direction.Up:
                    return plus(0, distance);
                case Direction.Left:
                    return plus(-distance, 0);
                case Direction.Down:
                    return plus(0, -distance);
                case Direction.Right:
                    return plus(distance, 0);
            }
            return new clsPoint(0, 0);
        }

        public clsPoint Move(string direction, int distance)
        {
            switch (direction)
            {
                case "R":
                case "E":
                case ">":
                    return plus(distance, 0);
                case "L":
                case "W":
                case "<":
                    return plus(-distance, 0);
                case "U":
                case "N":
                case "^":
                    return plus(0, distance);
                case "D":
                case "S":
                case "v":
                    return plus(0, -distance);
            }
            return new clsPoint(0, 0);
        }
    }

    public enum Direction
    {
        Up = 0,
        Left = 1,
        Down = 2,
        Right = 3
    }
}
