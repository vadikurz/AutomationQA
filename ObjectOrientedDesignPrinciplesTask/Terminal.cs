using System;
using System.Collections.Generic;
using System.Linq;
using ObjectOrientedDesignPrinciplesTask.Commands;
using ObjectOrientedDesignPrinciplesTask.Creators;
using ObjectOrientedDesignPrinciplesTask.Extensions;

namespace ObjectOrientedDesignPrinciplesTask;

public class Terminal
{
    private bool isDeactive;

    private bool isCarParkFull;

    private readonly CarPark carPark = new CarPark();

    private readonly CarManager carManager = new CarManager();

    private void Deactivate() => isDeactive = true;
    
    public void Run()
    {
        while (!isCarParkFull)
        {
            FillCarPark();
        }

        while (!isDeactive)
        {
            var command = ReadCommand();
            
            var creators = new List<ICreator>()
            {
                new AveragePriceCreator(),
                new CountAllCreator(),
                new AveragePriceTypeCreator(),
                new CountTypesCreator(),
                new ExitCreator()
            };
            
            carManager.Command = creators.Select(creator => creator.TryCreate(command))
                .SingleOrDefault(createdCommand => createdCommand is not null, new UndefinedCommand(command))!;
            carManager.GetInfo(carPark, Deactivate);
        }
    }

    private string ReadCommand()
    {
        Console.WriteLine("Enter command");
        return ReadType();
    }

    private void FillCarPark()
    {
        Console.WriteLine("Enter brand of car");
        var type = ReadType();

        Console.WriteLine("Enter model of car");
        var model = ReadType();

        Console.WriteLine("Enter the number of cars");
        var number = ReadType();

        Console.WriteLine("Enter cost of one unit");
        var price = ReadType();

        carPark.BatchesOfCars.Add(new BatchOfCars()
        {
            Type = type,
            Model = model,
            Number = Convert.ToInt32(number),
            Price = Convert.ToDouble(price)
        });
        
        Console.WriteLine("enter done if you added the cars");

        if (Console.ReadLine() == "done")
        {
            isCarParkFull = true;
        }
    }

    private string ReadType() => Console.In.ReadNotWhiteSpaceLine();
}