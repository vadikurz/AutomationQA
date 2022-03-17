using System;
using System.Collections.Generic;

namespace ObjectOrientedDesignPrinciplesTask
{
    public class CarPark
    {
        public List<BatchOfCars> BatchesOfCars { get; set; }

        public bool IsCarParkFull;

        public CarPark()
        {
            BatchesOfCars = new List<BatchOfCars>();
        }

        public void FillCarPark()
        {
            AddBatchOfCars();

            Console.WriteLine("press D if you added the cars");

            if (Console.ReadKey().Key == ConsoleKey.D)
            {
                IsCarParkFull = true;
            }
        }

        public void AddBatchOfCars()
        {
            Console.WriteLine("Enter brand of car");
            var type = Console.ReadLine();

            Console.WriteLine("Enter model of car");
            var model = Console.ReadLine();

            Console.WriteLine("Enter the number of cars");
            var number = Console.ReadLine();

            Console.WriteLine("Enter cost of one unit");
            var price = Console.ReadLine();

            BatchesOfCars.Add(new BatchOfCars()
            {
                Type = type,
                Model = model,
                Number = Convert.ToInt32(number),
                Price = Convert.ToDouble(price)
            });
        }
    }
}
