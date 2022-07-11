using UniRx;
using UnityEngine.Serialization;

namespace UserControlSystem
{
    public class ScriptableObjectReactiveBase<T> :  ScriptableObjectValueBase<T>
    {
        public ReactiveProperty<T> OnNewReactiveValue;

        public override void SetValue(T value)
        {
            base.SetValue(value);

            OnNewReactiveValue.Value = value;
        }
    }
}