using System.Threading;
using System.Threading.Tasks;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core.CommandExecutors
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        private const string ANIMATOR_WALK = "Walk";
        private const string ANIMATOR_IDLE = "Idle";

        [SerializeField] private UnitMovementStop _unitMovementStop;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            _stopCommandExecutor.CancellationTokenSource?.Cancel();
            await Task.Yield();
            Debug.Log($"{name} patroling!");
            await Patrolling(command.From, command.To);
        }

        private async Task Patrolling(Vector3 point_from, Vector3 point_to)
        {
            _animator.SetTrigger(ANIMATOR_WALK);
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            for (; ; )
            {
                try
                {
                    _navMeshAgent.destination = point_to;
                    await _unitMovementStop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
                    _navMeshAgent.destination = point_from;
                    await _unitMovementStop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
                }
                catch
                {
                    _navMeshAgent.isStopped = true;
                    _navMeshAgent.velocity = Vector3.zero;
                    _navMeshAgent.ResetPath();
                    break;
                }
            }
            _animator.SetTrigger(ANIMATOR_IDLE);
        }
    }
}