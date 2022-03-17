using System;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class CountTypes : ICommand
    {
        private static CountTypes instance;
        public CarPark CarPark { get; set; }

        private CountTypes(CarPark carPark)
        {
            CarPark = carPark;
        }

        public static CountTypes GetInstance(CarPark carPark) => instance ?? (instance = new CountTypes(carPark));

        public void Execute()
        {
            Console.WriteLine(CarPark.BatchesOfCars.Count);
        }
    }
}
