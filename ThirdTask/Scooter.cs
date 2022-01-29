using System;

namespace ThirdTask
{
    public class Scooter : Vehicle
    {
        public ushort MaxSpeed { get; }

        public Scooter(Engine engine, Transmission transmission, Chassis chassis, ushort maxSpeed) : base(engine, transmission, chassis)
        {
            if (chassis.NumberOfWheels < 2 || chassis.NumberOfWheels > 3)
            {
                throw new ArgumentOutOfRangeException("The number of wheels on the scooter can be from 2 before 3");
            }
            MaxSpeed = maxSpeed;
        }
        public override string ToString() => $"{base.ToString()}\n{MaxSpeed}\n";
    }
}