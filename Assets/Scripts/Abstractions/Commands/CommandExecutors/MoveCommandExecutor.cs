using Abstractions.Commands.CommandsInterfaces;
using Core;
using UnityEngine;
using UnityEngine.AI;

namespace Abstractions.Commands.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            base.ExecuteSpecificCommand(command);
            GetComponent<NavMeshAgent>().destination = command.Target;
            _animator.SetTrigger(Walk);
            await _stop;
            _animator.SetTrigger(Idle);
        }
    }
}