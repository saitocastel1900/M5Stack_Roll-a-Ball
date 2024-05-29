using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerCore : MonoBehaviour, IDamagable , IDieable
{
    /// <summary>
    /// 初期化したか
    /// </summary>
    public IObservable<Unit> OnInitializeAsync => _onInitializeAsyncSubject;
    private readonly AsyncSubject<Unit> _onInitializeAsyncSubject = new AsyncSubject<Unit>();

    /// <summary>
    /// 
    /// </summary>
    public IObservable<Unit> OnDead => _deadSubject;
    private Subject<Unit> _deadSubject = new Subject<Unit>();

    /// <summary>
    /// ダメージを受けたか
    /// </summary>
    public IObservable<Unit> OnDamagedCallBack => _damageSubject;
    private Subject<Unit> _damageSubject = new Subject<Unit>();

    /// <summary>
    ///
    /// </summary>
    public IObservable<ItemType> OnPickUpItemCallback => _onPickUpSubject;
    private Subject<ItemType> _onPickUpSubject = new Subject<ItemType>();

    public IReadOnlyReactiveProperty<bool> IsAlive => _isAlive;
    private ReactiveProperty<bool> _isAlive = new BoolReactiveProperty(true);

    private Vector3 _respawnPoint;

    private void Awake()
    {
        _deadSubject
            .Subscribe(_ =>
            {
                Debug.Log("死にました");
                Observable.Timer(TimeSpan.FromSeconds(2))
                    .Subscribe(___ => _isAlive.Value = true);
            })
            .AddTo(this.gameObject);

        _damageSubject
            .Subscribe(_ => Debug.Log("ダメージを受けました"))
            .AddTo(this);

        this.OnTriggerEnterAsObservable()
            .Subscribe(hit =>
            {
                var gettable = hit.gameObject.GetComponent<IGettable>();
                if (gettable != null)
                {
                    gettable.PickedUp();
                    _onPickUpSubject.OnNext(gettable.ItemType);
                }
            }).AddTo(this);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void InitializePlayer(Vector3 position,Vector3 respawnPoint)
    {
        this.gameObject.transform.position = position;
        _respawnPoint = respawnPoint;
        _onInitializeAsyncSubject.OnNext(Unit.Default);
        _onInitializeAsyncSubject.OnCompleted();
    }

    /// <summary>
    /// Damage
    /// </summary>
    public void ApplyDamage()
    {
        _damageSubject.OnNext(Unit.Default);
    }

    public void Kill()
    {
        this.gameObject.transform.position = _respawnPoint;
        _isAlive.Value = false;
    }
}