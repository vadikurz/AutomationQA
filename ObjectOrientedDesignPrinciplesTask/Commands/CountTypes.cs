using System;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class CountTypes : ICommand
    {
        public void Execute(CarPark carPark, Action deactivator)
        {
            Console.WriteLine(carPark.BatchesOfCars.Count);
        }
    }
}
