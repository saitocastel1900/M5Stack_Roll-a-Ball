using System;
using UniRx;
using Zenject;

public class TimerTextPresenter : IDisposable, IInitializable
{
    /// <summary>
    /// Model
    /// </summary>
    private ITimerTextModel _model;

    /// <summary>
    /// View
    /// </summary>
    private TimerTextView _view;

    /// <summary>
    /// 
    /// </summary>
    private TimerManager _timer;

    /// <summary>
    /// Disposable
    /// </summary>
    private CompositeDisposable _compositeDisposable;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public TimerTextPresenter(ITimerTextModel model, TimerTextView view, TimerManager timer)
    {
        _model = model;
        _view = view;
        _timer = timer;
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
        _model.TimeTextProp
            .Subscribe(_view.SetText)
            .AddTo(_compositeDisposable);
    }

    private void SetEvent()
    {
        _timer
            .RemainingTime
            .Subscribe( _model.SetTime)
            .AddTo(_compositeDisposable);
    }
    
    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}