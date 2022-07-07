using System;
using Abstractions.Commands.CommandsInterfaces;

namespace UserControlSystem
{
    public sealed class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IStopCommand> creationCallback)
        {
            
        }
    }
}