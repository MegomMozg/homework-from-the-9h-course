using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using Assets.Scripts.Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Zenject;

namespace Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        [Inject] private IHealthHolder _ourHealth;
        [Inject(Id = "AttackDistance")] private float _attackingDistance;
        [Inject(Id = "AttackPeriod")] private int _attackingPeriod;

        private Vector3 _ourPosition;
        private Vector3 _targetPosition;
        private Quaternion _ourRotation;

        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<IAttackable> _attackTargets = new Subject<IAttackable>();

        private Transform _targetTransform;
        private AttackOperation _currentAttackOp;
        
        [Inject]
        private void Init()
        {
            _targetPositions
                .Select(value => new Vector3((float)Math.Round(value.x, 2), (float)Math.Round(value.y, 2), (float)Math.Round(value.z, 2)))
                .Distinct()
                .ObserveOnMainThread()
                .Subscribe(StartMovingToPosition);

            _attackTargets
                .ObserveOnMainThread()
                .Subscribe(StartAttackingTargets);

            _targetRotations
                .ObserveOnMainThread()
                .Subscribe(SetAttackRotation);
        }
        
        private void SetAttackRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }

        private void StartAttackingTargets(IAttackable target)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().ResetPath();
            _animator.SetTrigger(Animator.StringToHash("Attack"));
            target.ReceiveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void StartMovingToPosition(Vector3 position)
        {
            GetComponent<NavMeshAgent>().destination = position;
            _animator.SetTrigger(Animator.StringToHash("Walk"));
        }
        
        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            if (command.Target != null)
            {
                _targetTransform = (command.Target as Component)?.transform;
                _currentAttackOp = new AttackOperation(this, command.Target);
                Update();
                _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
                try
                {
                    await _currentAttackOp.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
                }
                catch
                {
                    _currentAttackOp.Cancel();
                }
                _animator.SetTrigger("Idle");
                _currentAttackOp = null;
                _targetTransform = null;
                _stopCommandExecutor.CancellationTokenSource = null;
            }
        }
        
        private void Update()
        {
            if (_currentAttackOp == null)
            {
                return;
            }

            lock(this)
            {
                _ourPosition = transform.position;
                _ourRotation = transform.rotation;
                if (_targetTransform  != null)
                {
                    _targetPosition = _targetTransform .position;
                }
            }
        }

        #region AttackOperation

        public sealed class AttackOperation : IAwaitable<AsyncExtensions.Void>
	{
    		public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
    		{
        		private AttackOperation _attackOperation;

	        	public AttackOperationAwaiter(AttackOperation attackOperation)
       	 	{
            			_attackOperation = attackOperation;
            			attackOperation.OnComplete += ONComplete;
        		}

        		private void ONComplete()
        		{
            			_attackOperation.OnComplete -= ONComplete;
            			ONWaitFinish(new AsyncExtensions.Void());
        		}
    		}

	    	private event Action OnComplete;
	
    		private readonly AttackCommandExecutor _attackCommandExecutor;
    		private readonly IAttackable _target;

    		private bool _isCancelled;

    		public AttackOperation(AttackCommandExecutor attackCommandExecutor, IAttackable target)
    		{
        		_target = target;
        		_attackCommandExecutor = attackCommandExecutor;

	        	var thread = new Thread(AttackAlgorythm);
       	 		thread.Start();
    		}

    		public void Cancel()
    		{
        		_isCancelled = true;
        		OnComplete?.Invoke();
    		}

    		private void AttackAlgorythm(object obj)
    		{
        		while (true)
        		{
            			if (
                			_attackCommandExecutor == null
                			|| _attackCommandExecutor._ourHealth.Health == 0
                			|| _target.Health == 0
                			|| _isCancelled
                			)
            			{
                			OnComplete?.Invoke();
                			return;
            			}

            			var targetPosition = default(Vector3);
            			var ourPosition = default(Vector3);
            			var ourRotation = default(Quaternion);
            			lock (_attackCommandExecutor)
            			{
                			targetPosition = _attackCommandExecutor._targetPosition;
                			ourPosition = _attackCommandExecutor._ourPosition;
                			ourRotation = _attackCommandExecutor._ourRotation;
            			}

            			var vector = targetPosition - ourPosition;
            			var distanceToTarget = vector.magnitude;
            			if (distanceToTarget > _attackCommandExecutor._attackingDistance)
            			{
                			var finalDestination = targetPosition - vector.normalized * (_attackCommandExecutor._attackingDistance * 0.9f);
                			_attackCommandExecutor
						._targetPositions.OnNext(finalDestination);
                			Thread.Sleep(100);
            			}
            			else if (ourRotation != Quaternion.LookRotation(vector))
            			{
                			_attackCommandExecutor.
						_targetRotations
						.OnNext(Quaternion.LookRotation(vector));
            			}
            			else
            			{
                			_attackCommandExecutor._attackTargets.OnNext(_target);
                			Thread.Sleep(_attackCommandExecutor._attackingPeriod);
            			}
        		}
    		}

    		public IAwaiter<AsyncExtensions.Void> GetAwaiter()
    		{
        		return new AttackOperationAwaiter(this);
    		}
	}

        #endregion
    }
}