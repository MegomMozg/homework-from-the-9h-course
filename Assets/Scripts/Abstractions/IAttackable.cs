using Abstractions;
using UnityEngine;

public interface IAttackable : IHealthHolder
{
    Transform Transform { get; }
}