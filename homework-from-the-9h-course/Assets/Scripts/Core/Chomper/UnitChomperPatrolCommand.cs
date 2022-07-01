using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core
{
    public sealed class UnitChomperPatrolCommand : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log("Patrol");
        }
    }
}