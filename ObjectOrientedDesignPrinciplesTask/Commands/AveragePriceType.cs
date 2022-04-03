using System;
using System.Linq;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class AveragePriceType : ICommand
    {
        public string Type { get; set; }
        public AveragePriceType(string type)
        {
            Type = type;
        }

        public void Execute(CarPark carPark, Action deactivator) => 
            Console.WriteLine(carPark.BatchesOfCars.Where(batch => batch.Type == Type).Sum(batch => batch.Price) / 
                              carPark.BatchesOfCars.Count(batch => batch.Type == Type));
        
    }
}
