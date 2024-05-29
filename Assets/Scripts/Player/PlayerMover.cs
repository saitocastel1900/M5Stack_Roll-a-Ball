using UniRx;
using UnityEngine;

public class PlayerMover : BasePlayer
{
    [SerializeField] private float speed = 10f;

    protected override void OnInitialize()
    {
        _input.MoveDirection.Subscribe(direction =>
            GetComponent<Rigidbody>().AddForce(direction.x * speed, 0, direction.z * speed));
    }
}