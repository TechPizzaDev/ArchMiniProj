using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPickupEmote : EmoteEntry
{
    private GameObject player;

    public float range = 2f;

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindWithTag("Player");
    }

    protected override void InvokeClick(PointerEventData eventData)
    {
        base.InvokeClick(eventData);

        var orderStack = player.GetComponentInChildren<OrderStack>();
        orderStack.Push(Source.GetComponent<OrderedItem>());
    }

    protected override void Update()
    {
        base.Update();

        float dist = Vector3.Distance(Source.transform.position, player.transform.position);
        GetComponent<Image>().enabled = dist <= range;
    }
}