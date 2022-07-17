using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GeekBrains
{
    public class AsyncCancel : MonoBehaviour
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        public async void Run()
        {
            await Task.Run(() => SpamStringInDebug(_cancellationToken), _cancellationToken);
        }
        
        private void SpamStringInDebug(CancellationToken cancellationToken)
        {
            for (;;)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Debug.Log("Cancel me");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}