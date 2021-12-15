using System;

namespace ThirdTask
{
    public class Bus : Vehicle
    {
        public ushort NumberOfSeats { get; }

        public Bus(Engine engine, Transmission transmission, Chassis chassis, ushort numberOfSeats) : base(engine, transmission, chassis)
        {
            if (chassis.NumberOfWheels < 4)
            {
                throw new ArgumentOutOfRangeException("A bus cannot have less than 4 wheels.");
            }
            NumberOfSeats = numberOfSeats;
        }
        public override string ToString() => $"{NumberOfSeats}\n{Engine}\n{Transmission}\n{Chassis}\n";
    }
}
