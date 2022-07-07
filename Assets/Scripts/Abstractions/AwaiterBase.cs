using System;
using Utils;

namespace Utils
{
    public abstract class AwaiterBase<T> : IAwaiter<T>
    {
        public bool IsCompleted => _isCompleted;
        private bool _isCompleted;
        private Action _continuation;

        public abstract T GetResult();

        protected void SetComleted()
        {
            _isCompleted = true;
            _continuation?.Invoke();
        }

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
            {
                continuation?.Invoke();
            }
            else
            {
                _continuation = continuation;
            }
        }
    }
}
