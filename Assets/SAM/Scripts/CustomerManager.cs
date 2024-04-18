using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public EmoteManager emoteManager;
    public OrderManager orderManager;

    public GameObject agent;
    public Transform spawnPos;
    Vector2 vec2SpawnPos;

    private void Start()
    {
        if (emoteManager == null)
            emoteManager = FindObjectOfType<EmoteManager>();

        if (orderManager == null)
            orderManager = FindObjectOfType<OrderManager>();

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

        var stateManager = spawnedObject.GetComponent<StateManager>();
        stateManager.orderState.OnAnnounceOrder += emoteManager.InstantiateEmote;
        stateManager.orderState.OnCommitOrder += orderManager.AddOrder;
    }
}
