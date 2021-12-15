using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace FifthTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                var car = new Car(new Engine(300, 3, EngineType.Diesel, "1234567v"),
                    new Transmission(TransmissionType.Automatic, 7, "Aisin"),
                    new Chassis(4, "12345v2", 1000), CarBody.StationWagon);

                var bus = new Bus(new Engine(155, 2, EngineType.Electric, "v21r332502"),
                    new Transmission(TransmissionType.Manual, 8, "ZF"),
                    new Chassis(4, "004522vr34", 2000), 20);

                var scooter = new Scooter(new Engine(40, 0.5, EngineType.Electric, "87tr34wqe"),
                    new Transmission(TransmissionType.Manual, 8, "Aisin"),
                    new Chassis(2, "002245vr99", 200), 70);

                var truck = new Truck(new Engine(200, 3, EngineType.Electric, "dr3407tye"),
                    new Transmission(TransmissionType.Automatic, 8, "ZF"),
                    new Chassis(4, "456308ht95", 2500), 5);
                
                List<Vehicle> vehicles = new List<Vehicle>() { car, bus, scooter, truck };

                XmlSerializer serializer = new XmlSerializer(typeof(List<Vehicle>));
                using FileStream firstFileStream = new FileStream("vehicle_volume_more_than1,5.xml", FileMode.OpenOrCreate);
                serializer.Serialize(firstFileStream, vehicles.Where(vehicle => vehicle.Engine.Capacity > 1.5).ToList());

                serializer = new XmlSerializer(typeof(List<(EngineType, string, int)>));
                using FileStream secondFileStream = new FileStream("only_bus_and_truck_engine_information.xml", FileMode.OpenOrCreate);
                serializer.Serialize(secondFileStream, vehicles.Where(vehicle => (vehicle is Truck) || (vehicle is Bus))
                    .Select(vehicle => vehicle.Engine).Select(engine => (EngineType: engine.Type, SerialNumber: engine.SerialNumber, Power: engine.Power)).ToList());

                serializer = new XmlSerializer(typeof(List<Vehicle>));
                using FileStream ThirdFileStream = new FileStream("all_information_grouped_by_transmissiontype.xml", FileMode.OpenOrCreate);
                serializer.Serialize(ThirdFileStream, vehicles.OrderBy(vehicle => vehicle.Transmission.Type).ToList());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
