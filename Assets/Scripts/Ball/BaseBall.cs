using UniRx;
using UnityEngine;
using Zenject;

public abstract class BaseBall : MonoBehaviour
{
    //共通して使う物
        
    /// <summary>
    /// MoleCore
    /// </summary>
    protected BallCore _playerCore;
       
    /// <summary>
    /// Input
    /// </summary>
    [Inject] protected IInputEventProvider _input;
        
    private void Start()
    {
        _playerCore = this.gameObject.GetComponent<BallCore>();
        _playerCore.OnInitializeAsync.Subscribe(_=>OnInitialize()).AddTo(this);
            
        OnStart();
    }

    protected virtual void OnStart() { }

    protected abstract void OnInitialize();
}
