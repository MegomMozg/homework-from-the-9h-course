using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace UserControlSystem.CommandsRealization
{
    public sealed class PatrolCommand : IPatrolCommand
    {
        public Vector3 From { get; }
        public Vector3 Point { get; }

        public PatrolCommand(Vector3 @from, Vector3 to)
        {
            From = @from;
            Point = to;
        }
    }
}