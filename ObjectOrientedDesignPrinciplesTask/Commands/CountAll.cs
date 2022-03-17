using System;
using System.Linq;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class CountAll : ICommand
    {
        private static CountAll instance;
        public CarPark CarPark { get; set; }

        private CountAll(CarPark carPark)
        {
            CarPark = carPark;
        }

        public static CountAll GetInstance(CarPark carPark) => instance ?? (instance = new CountAll(carPark));

        public void Execute()
        {
            Console.WriteLine(CarPark.BatchesOfCars.Sum(batch => batch.Number));
        }
    }
}
