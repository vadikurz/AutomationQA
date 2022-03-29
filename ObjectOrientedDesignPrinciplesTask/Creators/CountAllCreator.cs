using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask.Creators
{
    public class CountAllCreator : ICreator
    {
        public ICommand? TryCreate(string command)
        {
            if (command is "count all")
            {
                return new CountAll();
            }

            return null;
        }
    }
}

