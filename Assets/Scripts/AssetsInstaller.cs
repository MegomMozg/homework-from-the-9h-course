using System;
using Abstractions;
using UnityEngine;
using UserControlSystem;
using Utils;
using Zenject;

[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
public sealed class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private AttackableValue _attackableClicksRMB;
    [SerializeField] private SelectableValue _selectables;

    private AutoMoveRMBData _autoMoveRMBData;

    public override void InstallBindings()
    {
        Container.Bind<IAwaitable<IAttackable>>()
            .FromInstance(_attackableClicksRMB);
        Container.Bind<IAwaitable<Vector3>>()
            .FromInstance(_groundClicksRMB);
        Container.Bind<IObservable<ISelectable>>().FromInstance(_selectables);
        Container.BindInstances(_legacyContext, _selectables);

        _autoMoveRMBData = new AutoMoveRMBData();
        _autoMoveRMBData.selectableValue = _selectables;
        _autoMoveRMBData.vector3Value = _groundClicksRMB;
        Container.Bind<AutoMoveRMBData>().FromInstance(_autoMoveRMBData);
    }
}
