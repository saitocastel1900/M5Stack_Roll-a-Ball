using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class DangerWall : MonoBehaviour
{
    private void Start()
    {
        this.gameObject
            .OnCollisionEnterAsObservable()
            .Subscribe(hit =>
            {
                var dieable = hit.gameObject.GetComponent<IDieable>();
                if (dieable != null)
                {
                    dieable.Kill();
                }
            });
    }
}