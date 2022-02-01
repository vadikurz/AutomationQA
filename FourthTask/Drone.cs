using System;

namespace FourthTask
{
    public class Drone : IFlyable
    {
        private const string SpeedValidationExceptionMessage = "The maximum speed of the drone is 260 kilometers per hour";
        private const string DistanceValidationExceptionMessage = "The drone cannot fly more than 1000 kilometers";
        public const int MaxDistance = 1000;
        public const int MinSpeed = 1;
        public const int MaxSpeed = 260;

        public Point CurrentPoint{ get; private set; }
        public int Speed { get; }

        public Drone(Point currentPoint, int speed)
        {
            if (speed is > MaxSpeed or < MinSpeed)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), SpeedValidationExceptionMessage);
            }
            CurrentPoint = currentPoint;
            Speed = speed;
        }

        public void FlyTo(Point destinationPoint)
        {
            double distance = CurrentPoint.GetDistance(destinationPoint);
            if (distance > MaxDistance)
            {
                throw new ArgumentOutOfRangeException(nameof(distance), DistanceValidationExceptionMessage);
            }
            CurrentPoint = destinationPoint;
        }

        public double GetFlyTime(Point destinationPoint)
        {
            double distance = CurrentPoint.GetDistance(destinationPoint);
            if (distance > MaxDistance)
            {
                throw new ArgumentOutOfRangeException(nameof(distance), DistanceValidationExceptionMessage);
            }
            return (distance / Speed) + (int)((distance / Speed) / 10);
        }

    }
}
