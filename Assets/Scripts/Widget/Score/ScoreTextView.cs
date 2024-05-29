using UnityEngine;
using UnityEngine.UI;

public class ScoreTextView : MonoBehaviour
{
    /// <summary>
    /// Text
    /// </summary>
    [SerializeField] private Text _scoreText;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        SetText(0);
    }
        
    /// <summary>
    /// スコアを表示する
    /// </summary>
    /// <param name="value"></param>
    public void SetText(int value)
    {
        _scoreText.text = "Score:" + value.ToString();
    }
}
