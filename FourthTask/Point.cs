using System;

namespace FourthTask
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Point(int x, int y, int z)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(x),"Coordinates cannot be negative");
            }
            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y), "Coordinates cannot be negative");
            }
            if (z < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(z), "Coordinates cannot be negative");
            }
            X = x;
            Y = y;
            Z = z;
        }
        public double GetDistance(Point destination)
        {
            return Math.Sqrt(Math.Pow(destination.X - X, 2) + Math.Pow(destination.Y - Y, 2) + Math.Pow(destination.Z - Z, 2));
        }

        public override string ToString() => $"X: {X}\nY: {Y}\nZ: {Z}\n";
    }
}
