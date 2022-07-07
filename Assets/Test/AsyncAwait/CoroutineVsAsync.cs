using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CoroutineVsAsync : MonoBehaviour
{
    [SerializeField]
    private GameObject _testObject;

    public void RunCoroutine()
    {
        StartCoroutine(TestRoutine());
    }

    private IEnumerator TestRoutine()
    {
        for (int i = 0; i < 10; i++)
        {
            _testObject.transform.Rotate(Vector3.down, 20);
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    public void RunAsync()
    {
        TestAsync();
    }

    private async void TestAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            _testObject.transform.Rotate(Vector3.down, 20);
            await Task.Delay(500);
        }

        await Task.Yield();
    }
}
