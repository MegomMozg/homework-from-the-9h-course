using System.Threading.Tasks;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override Task ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"{name} is attacking!");
            return Task.CompletedTask;
        }
    }
}