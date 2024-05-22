
public class CustomerEmote : EmoteEntry
{
    public StateManager Customer { get; set; }

    protected override void Start()
    {
        base.Start();

        Customer = Source.GetComponent<StateManager>();
    }
}