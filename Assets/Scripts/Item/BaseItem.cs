using UnityEngine;

public abstract class BaseItem : MonoBehaviour , IGettable
{
    public ItemType ItemType => _itemType; 
    [SerializeField] protected ItemType _itemType;
    
    public virtual void PickedUp()
    {
        Debug.Log(gameObject.name+"が取得されたよ");
        Destroy(this.gameObject);
    }
}
