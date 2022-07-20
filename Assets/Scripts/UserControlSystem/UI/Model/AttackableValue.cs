using System;
using Abstractions;
using UnityEngine;
using Utils;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Strategy Game/" + nameof(AttackableValue), order = 0)]
    public sealed class AttackableValue : StatelessScriptableObjectValueBase<IAttackable>
    {
        
    }
}