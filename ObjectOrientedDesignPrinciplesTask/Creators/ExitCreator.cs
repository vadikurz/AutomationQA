using ObjectOrientedDesignPrinciplesTask.Commands;

namespace ObjectOrientedDesignPrinciplesTask.Creators;

public class ExitCreator : ICreator
{
    public ICommand? TryCreate(string command)
    {
        if (command is "exit")
        {
            return new Exit();
        }

        return null;
    }
}