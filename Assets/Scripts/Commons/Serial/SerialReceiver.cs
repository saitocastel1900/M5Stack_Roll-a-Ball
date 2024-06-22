using UniRx;
using UnityEngine;

public class SerialReceiver : MonoBehaviour
{
    [SerializeField] private SerialHandler _serialHandler;
    [SerializeField] private GameObject _object;
    
    private void Start()
    {
        _serialHandler.MessagesProp.Subscribe(Rotate).AddTo(this.gameObject);
    }

    private void Rotate(string messages)
    {
        if (messages!=null)
        {
            string[] vec3 = messages.Split(','); 
            if (vec3.Length == 3)
            {
                float x = float.Parse(vec3[0]) ; 
                float y = float.Parse(vec3[1]) ; 
                float z = float.Parse(vec3[2]) ; 
            
                Vector3 acceleration = new Vector3(x, y, z);
                float pitch = Mathf.Atan2(acceleration.y, Mathf.Sqrt(acceleration.x * acceleration.x + acceleration.z * acceleration.z)) * Mathf.Rad2Deg;
                float roll = Mathf.Atan2(-acceleration.x, acceleration.z) * Mathf.Rad2Deg;
    
                var targetRotation = Quaternion.Euler(pitch, 0, roll);
                _object.transform.rotation = Quaternion.Lerp(_object.transform.rotation,targetRotation, .25f);
            }
        }
    }
}