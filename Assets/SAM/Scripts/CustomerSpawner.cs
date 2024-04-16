using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    
    public GameObject agent;
    public Transform spawnPos;
    Vector2 vec2SpawnPos;

    private void Start()
    {
        vec2SpawnPos = spawnPos.position;
    }

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
