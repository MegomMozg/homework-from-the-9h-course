using Abstractions;
using Abstractions.Commands;
using Core;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Abstractions
{
    public sealed class ChomperInstaller : MonoInstaller
    {
        [SerializeField] private AttackerParallelnfoUpdater _attackerParallelnfoUpdater;
        [SerializeField] private FactionMemberParallelInfoUpdater _factionMemberParallelInfoUpdater;
        
        public override void InstallBindings()
        {
            Container.Bind<IHealthHolder>().FromComponentInChildren();
            Container.Bind<float>().WithId("AttackDistance").FromInstance(5f);
            Container.Bind<int>().WithId("AttackPeriod").FromInstance(1400);
            
            Container.Bind<IAutomaticAttacker>().FromComponentInChildren();
            Container
                .Bind<ITickable>()
                .FromInstance(_attackerParallelnfoUpdater);
            Container
                .Bind<ITickable>()
                .FromInstance(_factionMemberParallelInfoUpdater);
            Container.Bind<IFactionMember>().FromComponentInChildren();
            Container.Bind<ICommandsQueue>().FromComponentInChildren();
        }
    }
}