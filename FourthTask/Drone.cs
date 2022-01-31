using System;

namespace FourthTask
{
    public class Drone : IFlyable
    {
        public const int MaxDistance = 1000;
        public const int MinSpeed = 1;
        public const int MaxSpeed = 260;

        public Point CurrentPoint{ get; private set; }
        public int Speed { get; }

        public Drone(Point currentPoint, int speed)
        {
            if (speed > MaxSpeed || speed < MinSpeed)
            {
                throw new ArgumentOutOfRangeException(nameof(speed),"The maximum speed of the drone is 260 kilometers per hour");
            }
            CurrentPoint = currentPoint;
            Speed = speed;
        }

        public void FlyTo(Point destinationPoint)
        {
            double distance = CurrentPoint.GetDistance(CurrentPoint, destinationPoint);
            if (distance > MaxDistance)
            {
                throw new ArgumentOutOfRangeException(nameof(distance), "The drone cannot fly more than 1000 kilometers");
            }
            CurrentPoint = destinationPoint;
        }

        public double GetFlyTime(Point destinationPoint)
        {
            double distance = CurrentPoint.GetDistance(CurrentPoint, destinationPoint);
            if (distance > MaxDistance)
            {
                throw new ArgumentOutOfRangeException(nameof(distance), "The drone cannot fly more than 1000 kilometers");
            }
            return (distance / Speed) + (int)((distance / Speed) / 10);
        }

    }
}
