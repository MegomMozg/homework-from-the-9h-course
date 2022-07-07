using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Abstractions.Commands.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        [SerializeField] private Animator _animator;
        private static readonly int Idle = Animator.StringToHash("Idle");

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            base.ExecuteSpecificCommand(command);

            var navMeshAgent = GetComponent<NavMeshAgent>();

            navMeshAgent.SetDestination(transform.position);
            _animator.SetTrigger(Idle);
        }
    }
}