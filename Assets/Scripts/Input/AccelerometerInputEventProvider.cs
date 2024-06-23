using System;
using UniRx;
using UnityEngine;
using Zenject;

public class AccelerometerInputEventProvider : IInputEventProvider, IInitializable, IDisposable
{
    public IReadOnlyReactiveProperty<Vector3> InclinationAmount => _inclinationAmount;
    private ReactiveProperty<Vector3> _inclinationAmount = new ReactiveProperty<Vector3>();

    [Inject] private SerialHandler _serialHandler;

    /// <summary>
    /// 
    /// </summary>
    private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public void Initialize()
    {
        Observable.EveryUpdate()
            .Select(_ =>
            {
                string[] vec3  = _serialHandler.MessagesProp.Value.Split(',');
                float x = float.Parse(vec3[0]);
                float y = float.Parse(vec3[1]);
                float z = float.Parse(vec3[2]);

                return new Vector3(x, y, z);
            })
            .Subscribe(_inclinationAmount.SetValueAndForceNotify).AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}