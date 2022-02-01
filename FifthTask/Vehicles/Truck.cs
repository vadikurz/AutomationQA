using System;
using FifthTask.Parts;

namespace FifthTask.Vehicles
{
    [Serializable]
    public class Truck : Vehicle
    {
        public ushort LiftingCapacity { get; }
        public Truck(){}
        public Truck(Engine engine, Transmission transmission, Chassis chassis, ushort liftingCapacity) : base(engine, transmission, chassis)
        {
            if (chassis.NumberOfWheels < 4)
            {
                throw new ArgumentOutOfRangeException("A truck cannot have less than 4 wheels.");
            }
            LiftingCapacity = liftingCapacity;
        }
        public override string ToString()
        {
            return $"{LiftingCapacity}\n{Engine}\n{Transmission}\n{Chassis}\n";
        }
    }
}
