using UnityEngine;

namespace Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor, ICancelCommand where T : ICommand
    {
        public void ExecuteCommand(object command) => ExecuteSpecificCommand((T)command);

        public virtual void ExecuteSpecificCommand(T command)
        {
            CancelAllCommand();
        }

        public virtual void CancelCommand(){}
        private void CancelAllCommand()
        {
            ICancelCommand[] commands = GetComponentsInChildren<ICancelCommand>();
            foreach (ICancelCommand command in commands)
            {
                command.CancelCommand();
            }
        }
    }
}