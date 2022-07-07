using Abstractions.Commands.CommandsInterfaces;
using Core;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utils;

namespace Abstractions.Commands.CommandExecutors
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [FormerlySerializedAs("_animator")] [SerializeField] private Animator _anim;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private CancellationTokenSource _ctSource;

        public override async void ExecuteSpecificCommand(IPatrolCommand command)
        {
            base.ExecuteSpecificCommand(command);

            await Patrol(command);
        }

        public override void CancelCommand()
        {
            if (_ctSource != null)
            {
                _ctSource.Cancel();
                _ctSource.Dispose();
                _ctSource = null;
            }
        }

        public async Task Patrol(IPatrolCommand command)
        {
            _ctSource = new CancellationTokenSource();

            var agent = GetComponent<NavMeshAgent>();

            try
            {
                for (; ; )
                {
                    _anim.SetTrigger(Walk);
                    agent.destination = command.Point;
                    await _stop.WithCancellation(_ctSource.Token);
                    agent.destination = command.From;
                    await _stop.WithCancellation(_ctSource.Token);
                }
            }
            catch
            {
                _anim.SetTrigger(Idle);
            }
        }
    }
}