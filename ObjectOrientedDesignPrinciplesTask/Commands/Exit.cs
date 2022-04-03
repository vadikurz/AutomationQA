using System;

namespace ObjectOrientedDesignPrinciplesTask.Commands;

public class Exit : ICommand
{
    public void Execute(CarPark carPark, Action deactivator)
    {
        deactivator.Invoke();
    }
}