using System;
using System.Collections.Generic;

namespace ThirdTask
{
    public class Program
    {
        public static void Main(string[] args)
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
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine(vehicle.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}