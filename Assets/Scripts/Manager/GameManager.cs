using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public IReactiveProperty<GameEnum.State> CurrentStateProp => _currentState;
    private ReactiveProperty<GameEnum.State> _currentState;

    [Inject] private TimerManager _timerManager;
    [Inject] private AudioManager _audioManager;

    [SerializeField] private ResultWidgetController _result;
    [SerializeField] private Goal _goal;

    private void Start()
    {
        _currentState = new ReactiveProperty<GameEnum.State>(GameEnum.State.ReadyAsync);
        
        _currentState
            .Subscribe(OnStateChanged)
            .AddTo(this.gameObject);
    }

    private async UniTask ReadyAsync()
    {
        //TODO:UIを表示する
        //TODO:ゲームを始める入力を受けたら
        //TODO:Playに遷移する
        
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        
        _currentState.Value = GameEnum.State.Play;
    }

    private void Play()
    {
        //TODO:入力待ちのUIを非表示にする
        //TODO:カウントダウン開始する
        //TODO:ボールを生成する
        
        _goal
            .IsReachedGoal
            .Skip(1)
            .Subscribe(_ => _currentState.Value = GameEnum.State.Result)
            .AddTo(this.gameObject);

        _timerManager
            .RemainingTime
            .SkipLatestValueOnSubscribe()
            .FirstOrDefault(x => x == 0)
            .Delay(TimeSpan.FromSeconds(2))
            .Subscribe(x => _currentState.Value = GameEnum.State.Result);
        
        _audioManager.PlayBGM(BGM.BGM1);
    }

    private void Result()
    {
        //TODO:BGMを止める
        _result.SetActive(true);
    }

    private void OnStateChanged(GameEnum.State currentState)
    {
        switch (currentState)
        {
            case GameEnum.State.ReadyAsync:
                ReadyAsync();
                break;
            case GameEnum.State.Play:
                Play();
                break;
            case GameEnum.State.Result:
                Result();
                break;
            default:
                break;
        }
    }
}