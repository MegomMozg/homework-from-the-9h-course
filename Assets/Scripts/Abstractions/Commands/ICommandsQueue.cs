namespace Abstractions.Commands
{
    public interface ICommandsQueue
    {
        ICommand CurrentCommand { get; }
        void EnqueueCommand(object command);
        void Clear();
    }
}