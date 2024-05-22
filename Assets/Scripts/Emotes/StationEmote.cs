using UnityEngine;
using UnityEngine.EventSystems;

public class StationEmote : EmoteEntry
{
    public SceneController sceneController;

    public int stationIndex = -1;

    public float jiggleFactor = 0.05f;

    protected override void Start()
    {
    }

    protected override void InvokeClick(PointerEventData eventData)
    {
        base.InvokeClick(eventData);

        sceneController.MoveToStation(stationIndex);
    }

    public override Vector3 CalculatePosition()
    {
        Vector3 pos = base.CalculatePosition();

        if (jiggleFactor != 0)
        {
            pos += new Vector3(0, Mathf.Sin(Time.time * 5) * jiggleFactor, 0);
        }

        return pos;
    }
}