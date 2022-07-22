using System;
using System.Threading;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThreadSamples : MonoBehaviour
{
    #region Volatile

    public class TestVolatile
    {
         //public bool Flag;
        public volatile bool Flag;

        public float Value = 0;
    }

    private TestVolatile _testVolatile = new TestVolatile();

    public void RunTest1()
    {
        var thread1 = new Thread((StartVolatile1));
        var thread2 = new Thread(StartVolatile2);
        thread1.Start();
        thread2.Start();
    }

    private void StartVolatile1(object obj)
    {
        // TODO Wrong!
        // _testVolatile.Flag = true;
        // _testVolatile.Value = 5;

        //Volatile.Write(ref _test.Flag, true);

        //TODO Right
        for (int i = 0; i < 1000; i++)
        {
            _testVolatile.Value = CalculatePerlinNoise(i);
        }
    }

    private void StartVolatile2(object obj)
    {
        //Volatile.Read(ref _test.Flag);
        //if (_testVolatile.Flag)
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log($"Value {_testVolatile.Value}");
        }
    }

    #endregion

    #region Interlocked

    public class InterlockedTest
    {
        public int Count;
    }

    private InterlockedTest _interlockedTest = new InterlockedTest();

    public void RunTest2()
    {
        _interlockedTest.Count = 3;
        
        var thread1 = new Thread(StartInterlocked1);
        var thread2 = new Thread(StartInterlocked2);
        var thread3 = new Thread(StartInterlocked3);
        
        thread1.Start();
        thread2.Start();
        thread3.Start();
    }

    private void StartInterlocked1(object obj) => Interlocked.Increment(ref _interlockedTest.Count);
    private void StartInterlocked2(object obj) => Interlocked.Increment(ref _interlockedTest.Count);

    private void StartInterlocked3(object obj)
    {
        if (_interlockedTest.Count == 5)
        {
            Debug.Log("Five!");
        }
    }

    #endregion

    #region Lock

    private object _lock = new object();
    
    private void LockSample()
    {
        lock (_lock)
        {
            //CalculatePerlinNoise();
        }

        int i = 5;
        lock (_lock)
        {
            for (i = 0; i < 10; i++)
            {
                i = i + 500;
                Debug.Log(i);
            }
        }
        // lock => 
        try
        {
            Monitor.Enter(_lock);
            //CalculatePerlinNoise();
        }
        finally
        {
            Monitor.Exit(_lock);
        }
        
        
    }

    private float CalculatePerlinNoise(int i)
    {
        return Mathf.PerlinNoise(i % 100, i % 1000);
    }

    #endregion

    #region UniRx

    private void UniRxThreadSamples()
    {
        var someMethod = Observable.Start(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Debug.Log("Method!");
                return 10;
            })
            .ObserveOn(Scheduler.MainThread) // default
            .SubscribeOn(Scheduler.ThreadPool);
    }

    #endregion
}
