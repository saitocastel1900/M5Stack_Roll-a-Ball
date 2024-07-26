using UniRx;
using UnityEngine;
using Zenject;

public class SerialReceiver : MonoBehaviour
{
    [Inject] private IInputEventProvider _input;
    [SerializeField] private GameObject _object;

    private void Start()
    {
        _input.InclinationAmount.Subscribe(Rotate).AddTo(this.gameObject);
    }

    private void Rotate(Vector3 acceleration)
    {
        float pitch = Mathf.Atan2(acceleration.y,
            Mathf.Sqrt(acceleration.x * acceleration.x + acceleration.z * acceleration.z)) * Mathf.Rad2Deg;
        float roll = Mathf.Atan2(-acceleration.x, acceleration.z) * Mathf.Rad2Deg;

        var targetRotation = Quaternion.Euler(pitch, 0, roll);
        _object.transform.rotation = Quaternion.Lerp(_object.transform.rotation, targetRotation, .5f);
    }
}