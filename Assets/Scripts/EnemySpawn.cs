using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] spawn_points;
    public GameObject enemy_Prefab;

    float respawn_time;
    void Start()
    {
        respawn_time = 4.0f;
    }

    void Update()
    {
        respawn_time -= Time.deltaTime;

        if(respawn_time < 0)
        {
            for(int i = 0; i < spawn_points.Length; i++)
            {
                Instantiate(enemy_Prefab, spawn_points[i].position, Quaternion.identity);
                respawn_time = 5.0f;
            }
        }
    }
}
