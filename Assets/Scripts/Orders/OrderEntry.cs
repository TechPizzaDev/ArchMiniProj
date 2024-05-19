
public class OrderEntry : EmoteEntry
{
    public StateManager Customer { get; set; }

    protected override void Start()
    {
        base.Start();

        Customer = Source.GetComponent<StateManager>();
    }
}