using System;

namespace ThirdTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var car = new Car(new Engine(300, 3, EngineType.Diesel, "1234567v"),
                    new Transmission(TransmissionType.Automatic, 7, "mercedes"),
                    new Chassis(4, "12345v2", 200),
                    CarBody.StationWagon);
                Console.WriteLine(car.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
