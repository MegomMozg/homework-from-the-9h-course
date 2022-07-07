using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

public sealed class AttackCommand : IAttackCommand
{
    public Vector3 Target { get; }
        
    public AttackCommand(Vector3 target)
    {
        Target = target;
    }
}