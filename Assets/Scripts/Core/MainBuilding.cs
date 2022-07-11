using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

public sealed class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Transform PivotPoint => _pivotPoint;
    public Sprite Icon => _icon;

    [SerializeField] private Transform _unitsParent;

    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Transform _pivotPoint;

    private float _health = 1000;

    public override void ExecuteSpecificCommand(IProduceUnitCommand command) 
        => Instantiate(command.UnitPrefab, 
            new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), 
            Quaternion.identity, 
            _unitsParent);
}