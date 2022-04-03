using System;
using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask
{
    public class CarManager
    {
        public ICommand Command { get; set; }

        public void GetInfo(CarPark carPark, Action deactivator)
        {
            Command.Execute(carPark, deactivator);
        }
    }
}
