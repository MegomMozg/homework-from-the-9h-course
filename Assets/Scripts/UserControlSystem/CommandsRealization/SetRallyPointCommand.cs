using Abstractions;
using UnityEngine;

namespace UserControlSystem.CommandsRealization
{
    public sealed class SetRallyPointCommand : ISetRallyPointCommand
    {
        public Vector3 RallyPoint { get; }

        public SetRallyPointCommand(Vector3 rallyPoint)
        {
            RallyPoint = rallyPoint;
        }
    }
}