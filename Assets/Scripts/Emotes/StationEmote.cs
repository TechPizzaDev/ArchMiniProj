using UnityEngine.EventSystems;

public class StationEmote : EmoteEntry
{
    public SceneController sceneController;

    public int stationIndex = -1;

    protected override void Start()
    {
    }

    protected override void InvokeClick(PointerEventData eventData)
    {
        base.InvokeClick(eventData);

        sceneController.MoveToStation(stationIndex);
    }
}