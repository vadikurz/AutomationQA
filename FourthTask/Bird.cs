
using System;

namespace FourthTask
{
    public class Bird : IFlyable
    {
        public const int MaxDistance = 100;
        public const int MinSpeed = 1;
        public const int MaxSpeed = 20;
        public Point CurrentPoint { get; private set; }
        public int Speed { get; }
        public Bird(Point currentPoint, int speed)
        {
            if (speed > MaxSpeed || speed < MinSpeed)
            {
                throw new ArgumentOutOfRangeException("The bird's speed can be from 1 to 20 kilometers per hour");
            }
            CurrentPoint = currentPoint;
            Speed = speed;
        }

        public void FlyTo(Point destinationPoint)
        {
            double distance = CurrentPoint.GetDistance(CurrentPoint, destinationPoint);
            if (distance > MaxDistance)
            {
                throw new ArgumentOutOfRangeException("The Bird cannot fly more than 100 kilometers");
            }
            CurrentPoint = destinationPoint;
        }

        public double GetFlyTime(Point destinationPoint)
        {
            double distance = CurrentPoint.GetDistance(CurrentPoint, destinationPoint);
            if (distance > MaxDistance)
            {
                throw new ArgumentOutOfRangeException("The Bird cannot fly more than 100 kilometers");
            }
            return CurrentPoint.GetDistance(CurrentPoint, destinationPoint) / Speed;
        }

    }
}
