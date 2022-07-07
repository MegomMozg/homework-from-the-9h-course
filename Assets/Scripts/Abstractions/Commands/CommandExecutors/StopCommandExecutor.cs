using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Abstractions.Commands.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log($"{name} has stopped!");
        }
    }
}