using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core
{
    public sealed class UnitChomperAttackCommand : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log("Attack");
        }
    }
}