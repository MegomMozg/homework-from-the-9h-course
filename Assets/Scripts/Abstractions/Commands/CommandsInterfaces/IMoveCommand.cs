using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IMoveCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}