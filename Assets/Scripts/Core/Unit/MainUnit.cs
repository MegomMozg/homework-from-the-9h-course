using Abstractions;
using Assets.Scripts.Abstractions;
using Core.CommandExecutors;
using UnityEngine;

namespace Core
{
    public class MainUnit : MonoBehaviour, ISelectable, IAttackable, IUnit, IDamageDealer, IAutomaticAttacker
    {
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Transform PivotPoint => _pivotPoint;
        public Sprite Icon => _icon;
        public int Damage => _damage;
        public float VisionRadius => _visionRadius;

        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommand;
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Transform _pivotPoint;
        [SerializeField] private int _damage = 25;
        [SerializeField] private float _visionRadius = 8f;
        private float _health = 100;
        
        public void ReceiveDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }
            _health -= amount;
            if (_health <= 0)
            {
                _animator.SetTrigger("PlayDead");
                Invoke(nameof(Destroy), 1f);
            }
        }

        private async void Destroy()
        {
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
            Destroy(gameObject);
        }
    }
}