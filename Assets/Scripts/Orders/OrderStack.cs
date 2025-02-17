using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderStack : MonoBehaviour
{
    private Stack<OrderedItem> items = new();

    public float itemHeight;

    public void Push(OrderedItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        items.Push(item);

        var itemTransform = item.transform;
        itemTransform.parent = transform;
        itemTransform.localPosition = GetStackTop(items.Count - 1);
    }

    public bool TryPop(out OrderedItem item)
    {
        return items.TryPop(out item);
    }

    public bool TryPeek(out OrderedItem item)
    {
        return items.TryPeek(out item);
    }

    public Vector3 GetStackTop(int itemCount)
    {
        return new Vector3(0, itemCount * itemHeight, 0);
    }
}
