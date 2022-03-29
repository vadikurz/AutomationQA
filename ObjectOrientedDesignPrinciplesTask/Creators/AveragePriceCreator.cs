using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask.Creators
{
    class AveragePriceCreator : ICreator
    {
        public ICommand? TryCreate(string command)
        {
            if (command is "average price")
            {
                return new AveragePrice();
            }

            return null;
        }
    }
}
