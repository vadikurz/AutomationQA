using System;
using System.Linq;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class CountAll : ICommand
    {
        public CarPark CarPark { get; set; }

        public CountAll(CarPark carPark)
        {
            CarPark = carPark;
        }

        public void Execute()
        {
            Console.WriteLine(CarPark.BatchesOfCars.Sum(batch => batch.Number));
        }
    }
}
