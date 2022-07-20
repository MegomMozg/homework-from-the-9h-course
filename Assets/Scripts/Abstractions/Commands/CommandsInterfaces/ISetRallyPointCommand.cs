using Abstractions.Commands;
using UnityEngine;

namespace Abstractions
{
    public interface ISetRallyPointCommand : ICommand
    {
        Vector3 RallyPoint { get; }
    }
}