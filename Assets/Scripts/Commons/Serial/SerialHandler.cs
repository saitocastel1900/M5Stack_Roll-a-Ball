using System;
using System.IO.Ports;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class SerialHandler : MonoBehaviour
{
    public IReactiveProperty<string> MessagesProp => _messagesProp;
    private ReactiveProperty<string> _messagesProp = new ReactiveProperty<string>();

    [SerializeField] private string _portName;
    [SerializeField] private int _baurate;

    private SerialPort _serialPort;
    private bool _isRunning = false;

    private async void Start()
    {
        await Open();
    }

    private void OnDestroy()
    {
        Close();
    }

    private async UniTask Open()
    {
        _serialPort = new SerialPort(_portName, _baurate, Parity.None, 8, StopBits.One);

        try
        {
            _serialPort.Open();
            _isRunning = true;
            await Read();
        }
        catch (Exception ex)
        {
            Debug.Log($"ポートが開けませんでした。設定しているポート名が間違っている場合があります: {ex.Message}");
        }
    }

    private async UniTask Read()
    {
        try
        {
            while (_isRunning && _serialPort != null && _serialPort.IsOpen)
            {
                var message = await UniTask.Run(() => _serialPort.ReadLine(),
                    cancellationToken: this.GetCancellationTokenOnDestroy());
                _messagesProp.SetValueAndForceNotify(message);
            }
        }catch (Exception ex)
        {
            Debug.LogError($"ポートがデータが読み取れませんでした: {ex.Message}");
        }
    }

    void Close()
    {
        _isRunning = false;
        if (_serialPort != null)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }

            _serialPort.Dispose();
        }
    }
}