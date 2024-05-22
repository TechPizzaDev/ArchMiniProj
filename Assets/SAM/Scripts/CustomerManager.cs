using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public EmoteManager emoteManager;
    public OrderManager orderManager;

    public GameObject agent;
    public Transform spawnPos;
    Vector2 vec2SpawnPos;

    private void Awake()
    {
        if (emoteManager == null)
            emoteManager = FindObjectOfType<EmoteManager>();

        if (orderManager == null)
            orderManager = FindObjectOfType<OrderManager>();

        vec2SpawnPos = spawnPos.position;
    }

    void Update()
    {
        //Implementera f�r att testa snabbt:
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    Spawn();
        //}
    }

    public void Spawn()
    {
        GameObject spawnedObject = Instantiate(agent, vec2SpawnPos, Quaternion.identity);

        var stateManager = spawnedObject.GetComponent<StateManager>();
        
        stateManager.orderState.announceOrder += emoteManager.InstantiateEmote<EmoteEntry>;

        stateManager.orderState.commitOrder += (src) =>
        {
            var emote = emoteManager.InstantiateEmote<CustomerEmote>(src);
            orderManager.AddOrder(emote);
            return emote;
        };
    }
}
