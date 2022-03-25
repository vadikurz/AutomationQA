using System;
using System.Linq;
using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask
{
    public class Program
    {
        public static ICommand GetCommand(string[] command, CarPark carPark) => (command[0], command[1]) switch
        {
            ("count", "all") => new CountAll(carPark),
            ("count", "types") => new CountTypes(carPark),
            ("average", "price") => new AveragePrice(carPark),
        };

        public static ICommand GetCommandWithThreeWords(string[] command, CarPark carPark) => (command[0], command[1], command[2]) switch
        {
            ("average", "price", _) _ when (carPark.BatchesOfCars.Any(batch => batch.Type == command[2])) =>
            new AveragePriceType(carPark, carPark.BatchesOfCars.Where(batch => batch.Type == command[2]).Select(batch => batch.Type).ToList().First())
        };

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

               switch (commandAsArray.Length)
                {
                    case 2:
                        carManager.SetCommand(GetCommand(commandAsArray, carPark));
                        carManager.GetInfo();
                        break;
                    case 3:
                        carManager.SetCommand(GetCommandWithThreeWords(commandAsArray, carPark));
                        carManager.GetInfo();
                        break;
                }
                if (command is "exit")
                {
                    break;
                }

               
                /*
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
                        case "average price":
                        {
                            carManager.SetCommand(AveragePrice.GetInstance(carPark));
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
                */

            }
        }
    }
}
