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
            .InclinationDirection
            .Subscribe(Rotate)
            .AddTo(this.gameObject);
    }

    private void Rotate(Vector3 direction)
    {
        float pitch = Mathf.Clamp(direction.z * _maxPitch, -_maxPitch, _maxPitch);
        float roll = Mathf.Clamp(-direction.x * _maxRoll, -_maxRoll, _maxRoll);
        
        var targetRotation = Quaternion.Euler(pitch, 0, roll);
        _stageTransform.rotation = Quaternion.Lerp(_stageTransform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }
}