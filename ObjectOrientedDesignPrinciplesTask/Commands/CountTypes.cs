using System;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class CountTypes : ICommand
    {
        public CarPark CarPark { get; set; }

        public CountTypes(CarPark carPark)
        {
            CarPark = carPark;
        }

        public void Execute()
        {
            Console.WriteLine(CarPark.BatchesOfCars.Count);
        }
    }
}
