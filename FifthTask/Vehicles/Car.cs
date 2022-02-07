﻿using System;
using FifthTask.Parts;

namespace FifthTask.Vehicles
{
    [Serializable]
    public class Car : Vehicle
    {
        public CarBody BodyType { get; }

        private Car(){}

        public Car(Engine engine, Transmission transmission, Chassis chassis, CarBody bodyType) : base(engine, transmission, chassis)
        {
            if (chassis.NumberOfWheels < 4)
            {
                throw new ArgumentOutOfRangeException("A car cannot have less than 4 wheels.");
            }
            BodyType = bodyType;
        }

        public override string ToString()
        {
            return $"{BodyType}\n{Engine}\n{Transmission}\n{Chassis}\n";
        }
    }
}
