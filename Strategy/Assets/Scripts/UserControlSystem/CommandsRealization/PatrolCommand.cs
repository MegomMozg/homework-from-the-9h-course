using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace UserControlSystem.CommandsRealization
{
    public sealed class PatrolCommand : IPatrolCommand
    {
        public Vector3 Target { get; }
        
        public PatrolCommand(Vector3 target)
        {
            Target = target;
        }
    }
}