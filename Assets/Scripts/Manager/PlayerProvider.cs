using UnityEngine;

public class PlayerProvider : MonoBehaviour
{
    /// <summary>
    /// 現在のPlayer
    /// </summary>
    public PlayerCore Player => _player;
    [SerializeField] private PlayerCore _player;
    
    public void SetPosition(Vector3 position, Vector3 respawnPosition)
    {
        var core = _player.GetComponent<PlayerCore>();
        core.InitializePlayer(position,respawnPosition);
    }
}