using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BallCore : MonoBehaviour, IDieable
{
    /// <summary>
    /// 初期化したか
    /// </summary>
    public IObservable<Unit> OnInitializeAsync => _onInitializeAsyncSubject;

    private readonly AsyncSubject<Unit> _onInitializeAsyncSubject = new AsyncSubject<Unit>();

    /// <summary>
    /// 
    /// </summary>
    public IObservable<Unit> IsDead => _deadSubject;

    private Subject<Unit> _deadSubject = new Subject<Unit>();

    public IReadOnlyReactiveProperty<bool> IsAlive => _isAlive;
    private ReactiveProperty<bool> _isAlive = new BoolReactiveProperty(true);

    private Vector3 _spawnPosition;

    private void Awake()
    {
        _onInitializeAsyncSubject.Subscribe(_ =>
        {
            _deadSubject
                .Subscribe(_ =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(2))
                        .Subscribe(_ => _isAlive.Value = true);
                })
                .AddTo(this.gameObject);

            _isAlive
                .SkipLatestValueOnSubscribe()
                .Where(x => x == true)
                .Subscribe(_ => transform.position = _spawnPosition)
                .AddTo(this.gameObject);

            this.OnTriggerEnterAsObservable()
                .Subscribe(hit =>
                {
                    var enterable = hit.gameObject.GetComponent<IEnterable>();
                    if (enterable != null)
                    {
                        enterable.Enter();
                    }
                }).AddTo(this);
        }).AddTo(this.gameObject);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void InitializeBall(Vector3 spawnPosition)
    {
        _spawnPosition = spawnPosition;
        _onInitializeAsyncSubject.OnNext(Unit.Default);
        _onInitializeAsyncSubject.OnCompleted();
    }

    public void Kill()
    {
        _deadSubject.OnNext(Unit.Default);
        _isAlive.Value = false;
    }
}