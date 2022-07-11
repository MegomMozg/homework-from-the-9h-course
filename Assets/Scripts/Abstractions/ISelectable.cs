using UnityEngine;

namespace Abstractions
{
    public interface ISelectable : IHealthHolder
    {
        Transform PivotPoint { get; }
        Sprite Icon { get; }
    }
}