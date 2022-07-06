using System;
using Abstractions.Commands.CommandsInterfaces;

namespace UserControlSystem
{
    public sealed class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            
        }
    }
}