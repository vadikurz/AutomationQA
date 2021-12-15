using System;
using System.Xml.Serialization;

namespace FifthTask
{
    [Serializable]
    [XmlInclude(typeof(Bus))]
    [XmlInclude(typeof(Car))]
    [XmlInclude(typeof(Scooter))]
    [XmlInclude(typeof(Truck))]
    public abstract class Vehicle
    {
        public Engine Engine { get; set; }

        public Transmission Transmission { get; set; }

        public Chassis Chassis { get; set; }
        public Vehicle() {}
        protected Vehicle(Engine engine, Transmission transmission, Chassis chassis)
        {
            Engine = engine;
            Transmission = transmission;
            Chassis = chassis;
        }
    }
}
