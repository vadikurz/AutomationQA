using System;

namespace FifthTask
{
    [Serializable]
    public class Bus : Vehicle
    {
        public ushort NumberOfSeats { get; set; }
        public Bus(){}
        public Bus(Engine engine, Transmission transmission, Chassis chassis, ushort numberOfSeats) : base(engine, transmission, chassis)
        {
            if (chassis.NumberOfWheels < 4)
            {
                throw new ArgumentOutOfRangeException("A bus cannot have less than 4 wheels.");
            }
            NumberOfSeats = numberOfSeats;
        }
        public override string ToString()
        {
            return $"{NumberOfSeats}\n{Engine}\n{Transmission}\n{Chassis}\n";
        }
    }
}
