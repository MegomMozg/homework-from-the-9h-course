using System;
using UnityEngine;
using Zenject;

public class InjectSamples : MonoBehaviour
{
    [Inject] private ITest _test;
    
    [Inject] public ITest Test { get; private set; }
    
    [Inject]
    private void Initialize(ITest test){}
    
    [Inject]
    public InjectSamples(ITest test){} //TODO Don't do this for MonoBehaviour!
}

public interface ITest {}

public class Test : ITest, IInitializable, IDisposable
{
    public void Initialize()
    {
        // Init logic
    }

    public void Dispose()
    {
        // Dispose
    }
}