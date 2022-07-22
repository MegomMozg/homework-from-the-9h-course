using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControlSystem.UI.Model;
using Zenject;

namespace UserControlSystem
{
    public sealed class UIModelInstaller : MonoInstaller
    {
        [SerializeField]
        private Sprite _chomperSprite;

        public override void InstallBindings()
        {
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>()
                .To<ProduceUnitCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>()
                .To<AttackCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>()
                .To<MoveCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>()
                .To<PatrolCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>()
                .To<StopCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<ISetRallyPointCommand>>()
                .To<SetRallyPointCommandCreator>().AsTransient();

            Container.Bind<CommandButtonsModel>().AsTransient();
            
            Container.Bind<float>().WithId("Chomper").FromInstance(5f);
            Container.Bind<string>().WithId("Chomper").FromInstance("Chomper");
            Container.Bind<Sprite>().WithId("Chomper").FromInstance(_chomperSprite);

            Container.Bind<BottomCenterModel>().AsSingle();
        }
    }
}