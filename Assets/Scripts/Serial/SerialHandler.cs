using System;
using System.IO.Ports;
using UniRx;
using UnityEngine;

public class SerialHandler : MonoBehaviour
{
    public IReactiveProperty<string> MessagesProp => _messagesProp;
    private ReactiveProperty<string> _messagesProp = new ReactiveProperty<string>();
    
    [SerializeField]private string _portName;
    [SerializeField]private int _baurate;

    private SerialPort _serialPort;
    private bool _isRunning = false;

    private void Start () 
    {
        Open();
    }
	
    private void OnDestroy()
    {
        Close();
    }

    /// <summary>
    /// シリアル通信を開始する
    /// </summary>
    private void Open()
    {
        _serialPort = new SerialPort (_portName, _baurate, Parity.None, 8, StopBits.One);

        try
        {
            _serialPort.Open();
            _isRunning  = true;
            //別スレッドで実行  
            Scheduler.ThreadPool.Schedule (Read).AddTo(this);
        } 
        catch(Exception ex)
        {
            Debug.Log ("ポートが開けませんでした。設定している値が間違っている場合があります");
        }
    }
    
    /// <summary>
    /// データ受信時に呼ばれる
    /// </summary>
    private void Read()
    {
        while (_isRunning && _serialPort != null && _serialPort.IsOpen)
        {
            //ReadLineで読み込む
            string message = _serialPort.ReadLine();
            _messagesProp.SetValueAndForceNotify(message);
        }
    }
    
    /// <summary>
    /// シリアル通信を終了する
    /// </summary>
    private void Close()
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