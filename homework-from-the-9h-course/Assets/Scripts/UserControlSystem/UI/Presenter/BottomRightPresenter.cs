using Abstractions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserControlSystem
{
    public sealed class BottomRightPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonMoveGameObject;
        [SerializeField] private GameObject _buttonAttackGameObject;
        [SerializeField] private GameObject _buttonPatrolGameObject;
        [SerializeField] private GameObject _buttonHoldGameObject;

        [SerializeField] private Button _buttonMove;
        [SerializeField] private Button _buttonAttack;
        [SerializeField] private Button _buttonPatrol;
        [SerializeField] private Button _buttonHold;

        [SerializeField] private SelectableValue _selectedValue;

        private void Start()
        {
            _selectedValue.OnSelected += OnSelected;
            OnSelected(_selectedValue.CurrentValue);
        }
        
        private void OnSelected(ISelectable selected)
        {
            bool isEmpty = selected == null;

            _buttonMoveGameObject.SetActive(!isEmpty);
            _buttonAttackGameObject.SetActive(!isEmpty);
            _buttonPatrolGameObject.SetActive(!isEmpty);
            _buttonHoldGameObject.SetActive(!isEmpty);
        }
    }
}