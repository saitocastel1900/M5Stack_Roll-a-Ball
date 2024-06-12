using UniRx;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public IReactiveProperty<GameEnum.State> CurrentStateProp => _currentState;
    private ReactiveProperty<GameEnum.State> _currentState;

    [Inject] private ScoreTextPresenter _score;
    [Inject] private PlayerProvider _player;
    
    [SerializeField] private Vector3 _position;
    [SerializeField] private ResultWidgetController _result;
    
    private void Start()
    {
        _currentState = new ReactiveProperty<GameEnum.State>(GameEnum.State.Initialize);
        
        _currentState
            .Subscribe(OnStateChanged)
            .AddTo(this.gameObject);
        
        _score
            .ScoreProp
            .Where(x=>x>=8)
            .Subscribe(_=>_currentState.Value =GameEnum.State.Finish)
            .AddTo(this.gameObject);
    }

    private void Initialize()
    {
        _player.SetPosition(_position,_position);
        
        _currentState.Value = GameEnum.State.PlayeGame;
    }

    private void PlayGame()
    {
        _score
            .ScoreProp
            .Where(x=>x>=8)
            .Subscribe(_=>_currentState.Value =GameEnum.State.Finish)
            .AddTo(this.gameObject);
    }

    private void Finish()
    {
        _result.SetActive(true);
    }

    private void OnStateChanged(GameEnum.State currentState)
    {
        switch (currentState)
        {
            case GameEnum.State.Initialize:
                Initialize();
                break;
            case GameEnum.State.PlayeGame:
                PlayGame();
                break;
            case GameEnum.State.Finish:
                Finish();
                break;
        }
    }
}