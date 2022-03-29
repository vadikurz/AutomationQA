using System;
using System.Collections.Generic;
using System.Linq;
using ObjectOrientedDesignPrinciplesTask.Commands;
using ObjectOrientedDesignPrinciplesTask.Creators;

namespace ObjectOrientedDesignPrinciplesTask
{
    public class Program
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

                var creators = new List<ICreator>()
                {
                    new AveragePriceCreator(),
                    new CountAllCreator(),
                    new AveragePriceTypeCreator(),
                    new CountTypesCreator(), 
                };
                carManager.SetCommand(creators.Select(creator => creator.TryCreate(command))
                    .SingleOrDefault(createdCommand => createdCommand is not null, new UndefinedCommand(command))!);
                carManager.GetInfo(carPark);
            }
        }
    }
}
