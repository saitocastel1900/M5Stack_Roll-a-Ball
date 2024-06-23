using UniRx;
using UnityEngine;
using Zenject;

public class StageRotater : MonoBehaviour
{
    [SerializeField] private Transform _stageTransform;
    
    [SerializeField] private float _maxPitch = 20f;
    
    [SerializeField] private float _maxRoll = 20f;
    
    [SerializeField] private float _rotationSpeed = 5f;
    
     [Inject] private IInputEventProvider _inputEventProvider;
    
    private void Start()
    {
        _inputEventProvider
            .InclinationAmount
            .Subscribe(Rotate)
            .AddTo(this.gameObject);
    }

    private void Rotate(Vector3 inclinationAmount)
    {
        /*
        float pitch = -Mathf.Atan2(inclinationAmount.y,
            Mathf.Sqrt(inclinationAmount.x * inclinationAmount.x + inclinationAmount.z * inclinationAmount.z)) * Mathf.Rad2Deg;
        float roll = -Mathf.Atan2(-inclinationAmount.x, inclinationAmount.z) * Mathf.Rad2Deg;
        
        pitch = Mathf.Clamp(pitch, -_maxPitch, _maxPitch);
        roll = Mathf.Clamp(roll, -_maxRoll, _maxRoll);
        */
        
        float pitch = Mathf.Clamp(inclinationAmount.z * _maxPitch, -_maxPitch, _maxPitch);
        float roll = Mathf.Clamp(-inclinationAmount.x * _maxRoll, -_maxRoll, _maxRoll);
        
        var targetRotation = Quaternion.Euler(pitch, 0, roll);
        _stageTransform.rotation = Quaternion.Lerp(_stageTransform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }
}