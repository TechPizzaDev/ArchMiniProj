using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderQueue : MonoBehaviour
{
    private Queue<List<OrderedDesc>> orders = new();

    public void Enqueue(List<OrderedDesc> order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        orders.Enqueue(order);
    }

    public bool TryDequeue(out List<OrderedDesc> order)
    {
        return orders.TryDequeue(out order);
    }
}
