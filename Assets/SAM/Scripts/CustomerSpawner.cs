using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    
    public GameObject agent;
    public Transform spawnPos;
    [SerializeField] Vector2 vec2SpawnPos;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject spawnedObject = Instantiate(agent, vec2SpawnPos, Quaternion.identity);
    }
}
