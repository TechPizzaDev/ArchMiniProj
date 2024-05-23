
public class CustomerEmote : EmoteEntry
{
    public StateManager Customer { get; set; }
    public OrderDesc orderDesc { get; set; } 

    protected override void Start()
    {
        base.Start();

        Customer = Source.GetComponent<StateManager>();
    }

    // TODO: range check
}