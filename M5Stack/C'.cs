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

    private Vector3 gyroData;
    private Vector3 rotationAngle;
    
    private void Rotate(string messages)
    {
        string[] vec3 = messages.Split(',');  // Split it into an array
        if (vec3.Length == 3)  // If array length is 3
        {
            float gx = float.Parse(vec3[0]);
            float gy = float.Parse(vec3[1]);
            float gz = float.Parse(vec3[2]);

            gyroData = new Vector3(gx, gy, gz);

            // 積分して回転角度を更新
            rotationAngle += gyroData * Time.deltaTime;

            // オブジェクトの回転を更新
            _object.transform.rotation = Quaternion.Euler(rotationAngle);
            
        }
    }

    void OnDestroy()
    {
        _isLoop = false;
        _serial.Close();
    }
}