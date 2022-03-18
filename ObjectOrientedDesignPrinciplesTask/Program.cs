using System;
using System.Linq;
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
                var commandAsArray = Console.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var command = String.Join(" ", commandAsArray!);

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

                    if(carPark.BatchesOfCars.Any(batch => command == "average price" + " " + $"{batch.Type}"))
                    {
                        var cars = carPark.BatchesOfCars.Where(batch => commandAsArray[2] == batch.Type).ToList();
                        foreach (var car in cars)
                        {
                            Console.WriteLine(car.Type);
                        }
                    }

                    if (command is "exit")
                    {
                        break;
                    }
            }
        }
    }
}
