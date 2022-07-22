using System;
using UniRx;

namespace Utils
{
    public abstract class StatelessScriptableObjectValueBase<T> : ScriptableObjectValueBase<T>, IObservable<T>
    {
        private Subject<T> _innerDataSource = new Subject<T>();
        public override void SetValue(T value)
        {
            base.SetValue(value);
            _innerDataSource.OnNext(value);
        }
        public IDisposable Subscribe(IObserver<T> observer) => _innerDataSource.Subscribe(observer);
    }
}