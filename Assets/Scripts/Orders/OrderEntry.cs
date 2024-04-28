using System;

[Serializable]
public class OrderEntry
{
    public StateManager Customer { get; }

    public OrderEntry(StateManager customer)
    {
        Customer = customer != null ? customer : throw new ArgumentNullException(nameof(customer));
    }
}