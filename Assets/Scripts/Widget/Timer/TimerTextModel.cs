using UniRx;

public class TimerTextModel : ITimerTextModel
{
    /// <summary>
    /// 
    /// </summary>
    public IReactiveProperty<int> TimeTextProp => _timerProp;

    private IntReactiveProperty _timerProp;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public TimerTextModel()
    {
        _timerProp = new IntReactiveProperty(0);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetTime(int time)
    {
        _timerProp.Value = time;
    }
}