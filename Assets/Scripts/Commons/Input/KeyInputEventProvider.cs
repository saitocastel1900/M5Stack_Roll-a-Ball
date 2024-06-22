using System;
using UniRx;
using UnityEngine;
using Zenject;

public class KeyInputEventProvider : IInputEventProvider, IInitializable, IDisposable
{
    public IReadOnlyReactiveProperty<Vector3> InclinationDirection => _inclinationDirection;
    private ReactiveProperty<Vector3> _inclinationDirection = new ReactiveProperty<Vector3>();

    /// <summary>
    /// 
    /// </summary>
    private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public void Initialize()
    {
        Observable.EveryUpdate()
            .Select(_ => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")))
            .Subscribe(x => _inclinationDirection.SetValueAndForceNotify(x)).AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}