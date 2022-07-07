using Abstractions.Commands.CommandsInterfaces;
using Core;
using UnityEngine;
using UnityEngine.AI;

namespace Abstractions.Commands.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public override async void ExecuteSpecificCommand(IAttackCommand command)
        {
            base.ExecuteSpecificCommand(command);
            GetComponent<NavMeshAgent>().destination = command.Target.Transform.position;
            _animator.SetTrigger(Walk);
            await _stop;
            _animator.SetTrigger(Idle);
        }
    }
}