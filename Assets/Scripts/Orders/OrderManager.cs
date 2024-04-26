using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class OrderManager : MonoBehaviour
{
    [SerializeField]
    private List<OrderEntry> orders = new();

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddOrder(OrderEntry entry)
    {
        orders.Add(entry);
    }

    public void RemoveOrder(OrderEntry entry)
    {
        orders.Remove(entry);
    }
}
