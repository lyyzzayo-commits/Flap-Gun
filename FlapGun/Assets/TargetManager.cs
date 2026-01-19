using UnityEngine;

public class TargetManager : MonoBehaviour

{
     [Header("Required")]
    [SerializeField] private GameObject targetPrefab;

    [Header("Optional")]
    [SerializeField] private Transform spawnRoot;          
    [SerializeField] private Transform[] spawnPoints;      
    
    [SerializeField] private float spawnRadius = 3f;

    [SerializeField] private Camera targetCamera; // 비우면 Camera.main
    [SerializeField] private float margin = 0.1f;
    
    private GameObject _currentTarget;
    public GameObject RespawnForRound(int roundIndex)
    {
        Camera cam = targetCamera != null ? targetCamera : Camera.main;
        if (cam == null)
        {
            Debug.LogError("[TargetSpawner] Camera not found.");
            return null;
        }

        float vx = Random.Range(margin, 1f - margin);
        float vy = Random.Range(margin, 1f - margin);

        Vector3 worldPos = cam.ViewportToWorldPoint(
            new Vector3(vx, vy, cam.nearClipPlane)
        );

        worldPos.z = 0f;

        Vector3 pos = worldPos;
        Quaternion rot = Quaternion.identity;

        _currentTarget = Instantiate(targetPrefab,pos,rot);

        return _currentTarget;

    }
}
