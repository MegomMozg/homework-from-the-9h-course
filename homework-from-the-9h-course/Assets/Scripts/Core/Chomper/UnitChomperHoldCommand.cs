using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core
{
    public sealed class UnitChomperHoldCommand : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log("Hold");
        }
    }
}