using UniRx;
using UnityEngine;

public class SerialReceiver : MonoBehaviour
{
    [SerializeField] private SerialHandler _serialHandler;
    [SerializeField] private GameObject _object;

    private void Start()
    {
        _serialHandler.MessagesProp.Subscribe(Debug.Log).AddTo(this.gameObject);
    }

    private void Update()
    {
        
    }

    private void Rotate(string messages)
    {
        string[] vec3 = messages.Split(',');  // Split it into an array
        if (vec3.Length == 3)  // If array length is 3
        {
            Debug.Log("来たよ");
            // Parse the values
            float pitch = float.Parse(vec3[0]);
            float roll = float.Parse(vec3[1]);
            float yaw = float.Parse(vec3[2]);

            // Set the target rotation
            var targetRotation = Quaternion.Euler(pitch, roll, yaw);
            _object.transform.rotation = Quaternion.Lerp(_object.transform.rotation,targetRotation,5);
        }
    }
}