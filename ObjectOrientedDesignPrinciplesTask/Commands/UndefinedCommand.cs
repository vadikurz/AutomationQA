using System;

namespace ObjectOrientedDesignPrinciplesTask.Commands
{
    public class UndefinedCommand : ICommand
    {
        public string CommandName { get; set; }

        public UndefinedCommand(string commandName)
        {
            CommandName = commandName;
        }

        public void Execute(CarPark carPark, Action deactivator)
        {
            Console.WriteLine($"Undefined command: \"{CommandName}\"");
        }
    }
}
