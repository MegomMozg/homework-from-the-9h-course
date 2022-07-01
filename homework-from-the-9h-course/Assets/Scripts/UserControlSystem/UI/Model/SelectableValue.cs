using System;
using System.Collections;
using System.Collections.Generic;
using Abstractions;
using UnityEngine;
using UserControlSystem;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy Game/" + nameof(SelectableValue), order = 0)]
    public class SelectableValue : ScriptableObject
    {
        public ISelectable CurrentValue { get; private set; }
        public Action<ISelectable> OnSelected;

        public void SetValue(ISelectable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }
    }
}