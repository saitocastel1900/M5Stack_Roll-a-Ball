using UniRx;

public class ScoreTextModel  : IScoreTextModel
{
    /// <summary>
    /// 
    /// </summary>
    public IReactiveProperty<int> ScoreProp => _scoreProp;
    private IntReactiveProperty _scoreProp;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ScoreTextModel()
    {
        _scoreProp = new IntReactiveProperty(0);
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddScore()
    {
        _scoreProp.Value++;
    }
}
