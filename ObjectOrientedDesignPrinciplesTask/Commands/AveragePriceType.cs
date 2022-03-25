using System;
using System.Linq;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class AveragePriceType : ICommand
    {
        public CarPark CarPark { get; set; }

        public string Type { get; set; }

        public AveragePriceType(CarPark carPark, string type)
        {
            CarPark = carPark;
            Type = type;
        }

        public void Execute() => 
            Console.WriteLine(CarPark.BatchesOfCars.Where(batch => batch.Type == Type).Sum(batch => batch.Price) / 
                              CarPark.BatchesOfCars.Count(batch => batch.Type == Type));
        
    }
}
