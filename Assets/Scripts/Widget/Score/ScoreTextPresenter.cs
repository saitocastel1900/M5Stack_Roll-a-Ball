using System;
using UniRx;
using Zenject;

public class ScoreTextPresenter : IDisposable, IInitializable
{
    /// <summary>
    /// 
    /// </summary>
    public IObservable<int> ScoreProp => _model.ScoreProp;
    
    /// <summary>
    /// Model
    /// </summary>
    private IScoreTextModel _model;

    /// <summary>
    /// View
    /// </summary>
    private ScoreTextView _view;

    /// <summary>
    /// Disposable
    /// </summary>
    private CompositeDisposable _compositeDisposable;

    /// <summary>
    /// 
    /// </summary>
    private PlayerProvider _playerProvider;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ScoreTextPresenter(IScoreTextModel model, ScoreTextView view, PlayerProvider player)
    {
        _model = model;
        _view = view;
        _playerProvider = player;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        _compositeDisposable = new CompositeDisposable();
        _view.Initialize();
        
        Bind();
        SetEvent();
    }

    /// <summary>
    /// Bind
    /// </summary>
    private void Bind()
    {
        _model.ScoreProp
            .Subscribe(_view.SetText)
            .AddTo(_compositeDisposable);
    }

    private void SetEvent()
    {
        _playerProvider
            .Player
            .OnPickUpItemCallback
            .Subscribe(_ => _model.AddScore()).AddTo(_compositeDisposable);
    }
    
    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}