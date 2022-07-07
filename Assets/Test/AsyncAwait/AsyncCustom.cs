using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.GeekBrains
{
    public struct CustomAwaiter<TMonoBehaviour> : INotifyCompletion where TMonoBehaviour : MonoBehaviour
    {
        private TMonoBehaviour _tParam;

        public CustomAwaiter(TMonoBehaviour tParam) => _tParam = tParam;

        public void OnCompleted(Action continuation) => continuation?.Invoke();

        public bool IsCompleted => true;

        public string GetResult() => _tParam.name;
    }
}