using System;
using System.Linq;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class AveragePrice :  ICommand
    {
        public void Execute(CarPark carPark) =>
            Console.WriteLine(carPark.BatchesOfCars.Sum(batch => batch.Price) / carPark.BatchesOfCars.Count);
    }
}
