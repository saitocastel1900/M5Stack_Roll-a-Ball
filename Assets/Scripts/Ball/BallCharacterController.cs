using UnityEngine;

public class BallCharacterController : MonoBehaviour
{
  [SerializeField] private Rigidbody _rigidbody;

  public void Move(Vector3 direction, float speed)
  {
    //_rigidbody.AddForce(direction.x * speed, 0, direction.z * speed);
    //_rigidbody.WakeUp();
  }
}
