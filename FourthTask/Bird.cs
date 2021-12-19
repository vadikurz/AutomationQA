
using System;
using System.Net.Http.Headers;

namespace FourthTask
{
    public class Bird : IFlyable
    {
        public const int MaxDistance = 100;
        public const int MinSpeed = 0;
        public const int MaxSpeed = 20;
        public Point CurrentPoint { get; private set; }

        private int speed;
        public int Speed
        {
            get => speed;
            set
            {
                if (value < MinSpeed || value > MaxSpeed)
                {
                    throw new ArgumentOutOfRangeException("The bird speed must be between 0 and 20 km/h");
                }
                speed = value;
            }
        }

        public Bird(Point currentPoint)
        {
            CurrentPoint = currentPoint;
            Speed = RandomSpeed();
        }
        private int RandomSpeed()
        { 
            Random rnd = new Random();
            return rnd.Next(MinSpeed, MaxSpeed);
        }
        public void FlyTo(Point destinationPoint)
        {
            double distance = CurrentPoint.GetDistance(CurrentPoint, destinationPoint);
            if (distance > MaxDistance)
            {
                throw new ArgumentOutOfRangeException("The Bird cannot fly more than 100 kilometers");
            }

            if (!PossibilityToFly())
            {
                throw new ArgumentException("The Bird Speed is 0 km/h");
            }
            CurrentPoint = destinationPoint;
        }

        public double GetFlyTime(Point destinationPoint)
        {
            if (!PossibilityToFly())
            {
                throw new ArgumentException("The Bird Speed is 0 km/h");
            }
            double distance = CurrentPoint.GetDistance(CurrentPoint, destinationPoint);
            if (distance > MaxDistance)
            {
                throw new ArgumentOutOfRangeException("The Bird cannot fly more than 100 kilometers");
            }
            return CurrentPoint.GetDistance(CurrentPoint, destinationPoint) / Speed;
        }

        private bool PossibilityToFly()
        {
            return Speed != 0;
        }

    }
}
