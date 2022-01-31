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
            if (x < 0 || y < 0 || z < 0)
            {
                throw new ArgumentOutOfRangeException("Coordinates cannot be negative");
            }
            X = x;
            Y = y;
            Z = z;
        }
        public double GetDistance(Point startingPosition, Point destination)
        {
            return Math.Sqrt(Math.Pow(destination.X - startingPosition.X, 2)
                             + Math.Pow(destination.Y - startingPosition.Y, 2)
                             + Math.Pow(destination.Z - startingPosition.Z, 2));
        }

        public override string ToString() => $"X: {X}\nY: {Y}\nZ: {Z}\n";
    }
}
