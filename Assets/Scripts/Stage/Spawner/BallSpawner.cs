using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _spawnOffsetY;
    
    private void Start()
    {
        var spawnPosition = transform.position + new Vector3(0, +_spawnOffsetY, 0);
        var ballObject = Instantiate(_ballPrefab, spawnPosition,Quaternion.identity);
        var core = ballObject.GetComponent<BallCore>();
        core.InitializeBall(spawnPosition);
    }
}
