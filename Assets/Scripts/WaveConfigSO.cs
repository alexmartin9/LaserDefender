using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float timeSpawnVariance = 0.5f;
    [SerializeField] float minTimeBetweenSpawns = 0.2f;

    public float GetMoveSpeed() { return moveSpeed; }

    public int GetEnemyCount() { return enemyPrefabs.Count; }

    public GameObject GetEnemyPrefab(int idx) { return enemyPrefabs[idx]; }

    public Transform GetStartingWaypoint() { 
        return pathPrefab.GetChild(0); 
    }

    public List<Transform> GetWaypointList()
    {

        List<Transform> waypoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab)
        {
            waypoints.Add(waypoint);
        }
        return waypoints;
    }

    public float GetRandomSpawnTime() {
        float spawnTime = Random.Range(timeBetweenSpawns - timeSpawnVariance, timeBetweenSpawns + timeSpawnVariance);
        return Mathf.Clamp(spawnTime, minTimeBetweenSpawns, float.MaxValue); 
    }

}
