using System;
using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask.Creators
{
    public class AveragePriceTypeCreator : ICreator
    {
        public ICommand? TryCreate(string command)
        {
            var commandAsArray = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (commandAsArray.Length is 3 && commandAsArray[0] + " " + commandAsArray[1] is "average price")
            {
                return new AveragePriceType(commandAsArray[2]);
            }

            return null;
        }
    }
}
