using System;
using Abstractions.Commands;
using Core;

namespace UserControlSystem
{
    public abstract class CommandCreatorBase<T> where T : class, ICommand
    {
        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            if (commandExecutor is ICommandExecutor<T> classSpeacificExecutor)
            {
                ClassSpecificCommandCreation(callback);
            }
            return commandExecutor;
        }

        protected abstract void ClassSpecificCommandCreation(Action<T> creationCallback);

        public virtual void ProcessCancel() { }
    }
}