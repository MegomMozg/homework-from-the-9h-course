using System;
using UnityEngine;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Strategy Game/" + nameof(Vector3Value), order = 0)]
    public sealed class Vector3Value : ScriptableObject
    { 
        public Vector3 CurrentValue { get; private set; }
        public Action<Vector3> OnNewValue;
        
        public void SetValue(Vector3 value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}