using System;
using FifthTask.Parts;

namespace FifthTask.Vehicles
{
    [Serializable]
    public class Scooter : Vehicle
    {
        private Scooter(){}

        public ushort MaxSpeed { get; }

        public Scooter(Engine engine, Transmission transmission, Chassis chassis, ushort maxSpeed) : base(engine, transmission, chassis)
        {
            if (chassis.NumberOfWheels < 2 || chassis.NumberOfWheels > 3)
            {
                throw new ArgumentOutOfRangeException("The number of wheels on the scooter can be from 2 before 3");
            }

            MaxSpeed = maxSpeed;
        }

        public override string ToString() => $"{MaxSpeed}\n{Engine}\n{Transmission}\n{Chassis}\n";
    }
}
