using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Samples : MonoBehaviour
{
    public ReactiveCollection<int> ReactiveCollection;

    private void Start()
    {
        var list = new List<int>().ToReactiveCollection();

        ReactiveCollection.ObserveReset().Subscribe(unit => Debug.Log("Clear called")).AddTo(this);
        ReactiveCollection.ObserveCountChanged(true).Subscribe(i => Debug.Log($"Count = {i}")).AddTo(this);
        ReactiveCollection.ObserveAdd().Subscribe(@event => Debug.Log($"Added index = {@event.Index} value = {@event.Value}")).AddTo(this);
        ReactiveCollection.ObserveMove().Subscribe(@event => Debug.Log($"Moved value = {@event.Value} new index = {@event.NewIndex} old index = {@event.OldIndex}")).AddTo(this);
        ReactiveCollection.ObserveRemove().Subscribe(@event => Debug.Log($"Remove index = {@event.Index} value = {@event.Value}")).AddTo(this);
        ReactiveCollection.ObserveReplace().Subscribe(@event => Debug.Log($"Replace index = {@event.Index} new value = {@event.NewValue} old value = {@event.OldValue}")).AddTo(this);
        
        ReactiveCollection.Clear();
        ReactiveCollection.Add(2);
        ReactiveCollection.Add(5);
        ReactiveCollection.Move(0, 1);
        ReactiveCollection.Remove(2);
        ReactiveCollection[0] = 3;
        ReactiveCollection.Clear();
    }
}
