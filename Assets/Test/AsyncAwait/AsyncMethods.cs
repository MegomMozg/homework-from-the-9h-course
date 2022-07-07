using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncMethods : MonoBehaviour
{
    #region Await Custom

    [SerializeField] private AsyncSyntax _asyncSyntax;

    private async void Test()
    {
        await _asyncSyntax;
    }

    #endregion

    #region Methods

    public async void MethodStart()
    {
        await Task.Run(() => gameObject.SetActive(false)); // ???
        await Task.Factory.StartNew(() => Debug.Log("Task from factory"));

        var task1 = Task.FromCanceled(CancellationToken.None);
        var task2 = Task.FromException(new NullReferenceException());

        await Task.WhenAny(task1, task2);
        await Task.WhenAll(task1, task2);

        Task.WaitAll(task1, task2);
        Task.WaitAny(task1, task2);
    }
    
    #endregion
}
