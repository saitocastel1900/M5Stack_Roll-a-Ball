using UniRx;

public interface IScoreTextModel
{
    /// <summary>
    /// 
    /// </summary>
    public IReactiveProperty<int> ScoreProp { get; }

   
    /// <summary>
    /// 
    /// </summary>
    public void AddScore();
}
