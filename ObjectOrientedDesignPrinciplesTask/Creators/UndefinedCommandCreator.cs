using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask.Creators
{
    public class UndefinedCommandCreator : ICreator
    {
        public ICommand? TryCreate(string command)
        {
            return new UndefinedCommand(command);
        }
    }
}
