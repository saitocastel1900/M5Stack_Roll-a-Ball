using UnityEngine.UI;
using UniRx;
using UnityEngine;

public class ResultWidgetController : MonoBehaviour
{
    [SerializeField] private Text _text;
    public IReadOnlyReactiveProperty<bool> IsActive => _isActive;
    private BoolReactiveProperty _isActive = new BoolReactiveProperty();

    private void Start()
    {
        _isActive
            .Subscribe(SetActive)
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// 表示を設定
    /// </summary>
    public void SetActive(bool isView)
    {
        _text.gameObject.SetActive(isView);
    }
}