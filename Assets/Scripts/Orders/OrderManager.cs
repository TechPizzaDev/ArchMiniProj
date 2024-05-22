using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class OrderManager : MonoBehaviour
{
    [SerializeField]
    private List<CustomerEmote> orders = new();

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddOrder(CustomerEmote entry)
    {
        orders.Add(entry);
    }

    public void RemoveOrder(CustomerEmote entry)
    {
        orders.Remove(entry);
    }
}
