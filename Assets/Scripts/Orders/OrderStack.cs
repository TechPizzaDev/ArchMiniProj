using System.Collections.Generic;
using UnityEngine;

public class OrderStack : MonoBehaviour
{
    private Stack<GameObject> items = new();

    public float itemHeight;

    public void Push(GameObject item)
    {
        items.Push(item);

        var itemTransform = item.transform;
        itemTransform.parent = transform;
        itemTransform.localPosition = GetStackTop(items.Count - 1);
    }

    public bool TryPop(out GameObject result)
    {
        return items.TryPop(out result);
    }

    public Vector3 GetStackTop(int itemCount)
    {
        return new Vector3(0, itemCount * itemHeight, 0);
    }
}
