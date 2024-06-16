using UnityEngine;
using UnityEngine.UI;

public class TimerTextView : MonoBehaviour
{
    /// <summary>
    /// Text
    /// </summary>
    [SerializeField] private Text _timerText;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        SetText(0);
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    public void SetText(int time)
    {
        _timerText.text = time.ToString();
    }
}
