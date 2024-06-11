using System.IO.Ports;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class SerialReceiver : MonoBehaviour
{
    public IReactiveProperty<string> MessagesProp => _messagesProp;
    private ReactiveProperty<string> _messagesProp= new ReactiveProperty<string>();
   
    [SerializeField] private GameObject _object;
    
    public string PortName;
    public int Baurate;

    private SerialPort _serial;
    private bool _isLoop = true;

    private async void Start()
    {
        _serial = new SerialPort(PortName, Baurate, Parity.None, 8, StopBits.One);

        _serial.Open();
        await ReadData();
    }
    
    private async UniTask ReadData()
    {
        
        while (_isLoop)
        {
            var message = await UniTask.Run(() => _serial.ReadLine(), cancellationToken: this.GetCancellationTokenOnDestroy());
            Debug.Log(message);
            //_messagesProp.Value=message;
            Rotate(message);
        }
    }

    private Vector3 acceleration;
    private Vector3 initialAcceleration;
    private bool isCalibrated = false;
    
    private void Rotate(string messages)
    {
        string[] vec3 = messages.Split(',');  // Split it into an array
        if (vec3.Length == 3)  // If array length is 3
        {
            float x = float.Parse(vec3[0]) ; // 追加
            float y = float.Parse(vec3[1]) ; // 追加
            float z = float.Parse(vec3[2]) ; // 追加
            
            acceleration = new Vector3(x, y, z);
            float pitch = Mathf.Atan2(acceleration.y, Mathf.Sqrt(acceleration.x * acceleration.x + acceleration.z * acceleration.z)) * Mathf.Rad2Deg;
            float roll = Mathf.Atan2(-acceleration.x, acceleration.z) * Mathf.Rad2Deg;

            // オブジェクトの回転をピッチとロールに応じて更新
            //var targetRotation = Quaternion.Euler(pitch, 0, roll);
            _object.transform.rotation = Quaternion.Lerp(_object.transform.rotation,targetRotation,5);
            var targetRotation = Quaternion.Euler(pitch, 0, roll);
            Debug.Log(targetRotation);
            _object.transform.rotation = targetRotation;
           
        }
    }

    void OnDestroy()
    {
        _isLoop = false;
        _serial.Close();
    }
}