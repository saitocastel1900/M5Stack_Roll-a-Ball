using System;
using UniRx;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public IReactiveProperty<int> RemainingTime => _remainingTime;
    private IntReactiveProperty _remainingTime = new IntReactiveProperty();

    [SerializeField] private int _time = 30;

    public void Start()
    {
        Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
            .Select(x => (int)(_time - x))
            .TakeWhile(x => x >= 0)
            .Subscribe(x => _remainingTime.Value = x)
            .AddTo(this.gameObject);
    }
}