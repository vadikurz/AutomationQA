using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask
{
    public interface ICreator
    {
        public ICommand? TryCreate(string command);
    }
}
