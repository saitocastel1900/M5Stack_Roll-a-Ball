using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 offset;

    private void Start()
    {
        offset = GetComponent<Transform>().position - _target.position;
        
        this.gameObject.
            UpdateAsObservable().
            Subscribe(_=>
            {
                this.gameObject.transform.position = _target.position + offset;
            }).
            AddTo(this.gameObject);
    }
}
