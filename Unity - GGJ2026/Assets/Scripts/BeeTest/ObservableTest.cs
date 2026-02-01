using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;

public class ObservableTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Observable.EveryUpdate()
            .TakeUntil(this.OnDestroyAsObservable())
            .Subscribe(_ =>
        {
            Debug.Log("Testing!");
        });

        TestAwait().Forget();
    }

    private async UniTask TestAwait()
    {
        await UniTask.DelayFrame(100);
        Debug.Log($"{nameof(TestAwait)}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
