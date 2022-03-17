using System;
using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var carPark = new CarPark();
            var carManager = new CarManager();

            while (!carPark.IsCarParkFull)
            {
                carPark.FillCarPark();
            }

            while (true)
            {
                Console.WriteLine("Enter command");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "count all":
                    {
                        carManager.SetCommand(CountAll.GetInstance(carPark));
                        carManager.GetInfo();
                    } 
                        break;
                    case "count types":
                    {
                        carManager.SetCommand(CountTypes.GetInstance(carPark));
                        carManager.GetInfo();
                    }
                        break;
                }

                if (command == "exit")
                {
                    break;
                }
            }
        }
    }
}
