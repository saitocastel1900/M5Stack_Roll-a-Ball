using UniRx;

public interface ITimerTextModel
{
    /// <summary>
    /// 
    /// </summary>
    public IReactiveProperty<int> TimeTextProp { get; }

   
    /// <summary>
    /// 
    /// </summary>
    public void SetTime(int time);
}
