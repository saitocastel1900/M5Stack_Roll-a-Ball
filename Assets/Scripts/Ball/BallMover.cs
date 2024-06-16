using UniRx;
using UnityEngine;

public class BallMover : BaseBall
{
    [SerializeField] private BallCharacterController _characterController;
    [SerializeField] private float speed = 10f;

    protected override void OnInitialize()
    {
        _input.InclinationDirection.Subscribe(direction =>
        {
            _characterController.Move(direction, speed);
        }).AddTo(this.gameObject);
    }
}