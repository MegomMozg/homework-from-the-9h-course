using System;
using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem;
using UserControlSystem.UI.Presenter;

public sealed class MouseInteractionPresenter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;
    [SerializeField] private EventSystem _eventSystem;
    
    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private AttackableValue _attackablesRMB;
    [SerializeField] private Transform _groundTransform;
    
    private AutoCommand _autoCommand;
    private Plane _groundPlane;
    private CommandButtonsPresenter _commandButtonsPresenter;

    private void Awake()
    {
        _commandButtonsPresenter = FindObjectOfType<CommandButtonsPresenter>();
        _autoCommand = new AutoCommand();
    }

    private void Start() => _groundPlane = new Plane(_groundTransform.up, 0);

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
        {
            return;
        }
        
        if (_eventSystem.IsPointerOverGameObject())
        {
            return;
        }
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        if (Input.GetMouseButtonUp(0))
        {
            if (WeHit<ISelectable>(hits, out var selectable))
            {
                _selectedObject.SetValue(selectable);
            }
            else
            {
                _selectedObject.SetValue(null);
            }
        }
        else
        {
            if (WeHit<IAttackable>(hits, out var attackable))
            {
                _attackablesRMB.SetValue(attackable);
            }
            else if (_groundPlane.Raycast(ray, out var enter))
            {
                Vector3 newValue = ray.origin + ray.direction * enter;
                if (_commandButtonsPresenter != null && _commandButtonsPresenter.CommandIsPending)
                {
                    _groundClicksRMB.SetValue(newValue);
                }
                else if (!_autoCommand.AutoMoveUnit(_selectedObject.CurrentValue, newValue))
                {
                    _groundClicksRMB.SetValue(newValue);
                }
            }
        }
    }

    private bool WeHit<T>(RaycastHit[] hits, out T result) where T : class
    {
        result = default;
        if (hits.Length == 0)
        {
            return false;
        }    
        result = hits
            .Select(hit => hit.collider.GetComponentInParent<T>())
            .FirstOrDefault(c => c != null);
        return result != default;
    }
}