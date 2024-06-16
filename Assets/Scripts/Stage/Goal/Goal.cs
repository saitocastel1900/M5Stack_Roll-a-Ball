using System;
using UniRx;
using UnityEngine;

public class Goal : MonoBehaviour , IEnterable
{
    public IObservable<bool> IsReachedGoal=>_isReachedGoal;
    private BoolReactiveProperty _isReachedGoal = new BoolReactiveProperty(false);

    public void Enter()
    {
        _isReachedGoal.Value = true;
    }
}
