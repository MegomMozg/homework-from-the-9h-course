using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GeekBrains
{
    public static class Extensions
    {
        public static CustomAwaiter<MonoBehaviour> GetAwaiter(this MonoBehaviour monoBehaviour)
            => new CustomAwaiter<MonoBehaviour>(monoBehaviour);













        public struct Void
        {
        }

        public static async Task<TResult> WithCancellation<TResult>(this Task<TResult> originalTask, CancellationToken ct)
        {
            var cancelTask = new TaskCompletionSource<Void>();
            using (ct.Register(t => ((TaskCompletionSource<Void>) t).TrySetResult(new Void()), cancelTask))
            {
                var any = await Task.WhenAny(originalTask, cancelTask.Task);
                if (any == cancelTask.Task)
                {
                    ct.ThrowIfCancellationRequested();
                }
            }
            
            return await originalTask;
        }
    }
}
