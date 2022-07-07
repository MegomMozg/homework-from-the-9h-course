using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Abstractions.Commands.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command) 
            => Debug.Log($"{name} is moving to {command.Target}!");
    }
}