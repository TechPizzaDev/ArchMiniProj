using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickupEmote : EmoteEntry
{
    protected override void InvokeClick(PointerEventData eventData)
    {
        base.InvokeClick(eventData);

        GameObject playerObj = GameObject.FindWithTag("Player");

        var orderStack = playerObj.GetComponentInChildren<OrderStack>();
        orderStack.Push(Source);
    }
}