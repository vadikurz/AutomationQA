using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask.Creators
{
    public class CountTypesCreator : ICreator
    {
        public ICommand? TryCreate(string command)
        {
            if (command is "count types")
            {
                return new CountTypes();
            }

            return null;
        }
    }
}
