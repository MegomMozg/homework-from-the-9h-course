using System;
using System.Threading.Tasks;
using Assets.GeekBrains;
using UnityEngine;

public class AsyncSyntax : MonoBehaviour
{
    private void Start()
    {
        RunTest();
    }

    public void RunTest() => RunAsync();

    private async void RunAsync()
    {
        Debug.Log("Running test 1");
        Test1Async();
        Debug.Log("Running test 2");
        await Test2Async();
        Debug.Log("Running test 3");
        var result = await Test3Async();
        Debug.Log("Result from 3 = " + result);
    }

    private async void Test1Async()
    {
        Debug.Log("Before async 1");
        await Task.Delay(2000);
        Debug.Log("After async 1");
    }

    private async Task Test2Async()
    {
        Debug.Log("Before async 2");
        await Task.Delay(3000);
        Debug.Log("After async 2");
    }

    private async Task<int> Test3Async()
    {
        await Task.Delay(4000);
        return await Task.FromResult(45);
    }

    #region Custom Await

    public CustomAwaiter<MonoBehaviour> GetAwaiter() => new CustomAwaiter<MonoBehaviour>(this);

    #endregion
}
