using System;
using System.Linq;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class CountAll : ICommand
    {
        public void Execute(CarPark carPark)
        {
            Console.WriteLine(carPark.BatchesOfCars.Sum(batch => batch.Number));
        }
    }
}
