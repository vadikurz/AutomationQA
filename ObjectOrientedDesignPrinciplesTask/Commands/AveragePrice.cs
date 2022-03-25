using System;
using System.Linq;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class AveragePrice :  ICommand
    {
        public CarPark CarPark { get; set; }

        public AveragePrice(CarPark carPark)
        {
            CarPark = carPark;
        }

        public void Execute() =>
            Console.WriteLine(CarPark.BatchesOfCars.Sum(batch => batch.Price) / CarPark.BatchesOfCars.Count);
    }
}
