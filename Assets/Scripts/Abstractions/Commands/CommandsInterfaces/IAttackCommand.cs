using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IAttackCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}