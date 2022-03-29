using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask
{
    public class CarManager
    {
        public ICommand Command { get; set; }

        public void SetCommand(ICommand command)
        {
            Command = command;
        }

        public void GetInfo(CarPark carPark)
        {
            Command.Execute(carPark);
        }
    }
}
