using System;
using Abstractions;
using UnityEngine;
using UnityEngine.UI;
using UserControlSystem;
using Random = UnityEngine.Random;

namespace Core
{
    public sealed class MainBuilding : MonoBehaviour, IUnitProducer, ISelectable
    {
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private SelectableValue _selectedValue;
        [SerializeField] private Outline _outline;

        [SerializeField] private float _maxHealth = 1000;
        [SerializeField] private Sprite _icon;

        private float _health = 1000;
        

        private void Start()
        {
            _selectedValue.OnSelected += OnselectedOutline;
            
        }

        public void ProduceUnit()
        {
            
            Instantiate(_unitPrefab,
                new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                Quaternion.identity,
                _unitsParent);
        }

        private void OnselectedOutline(ISelectable selectable)
        {
            _outline.enabled = selectable != null;
        }
    }
}