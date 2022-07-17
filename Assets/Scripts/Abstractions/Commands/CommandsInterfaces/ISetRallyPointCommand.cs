using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface ISetRallyPointCommand : ICommand
    {
        Vector3 RallyPoint { get; }
    }
}