using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

public class StationDropoff : MonoBehaviour
{
    private PointList? pointList;
    private List<GameObject> items = new();

    public EmoteManager emoteManager;
    public Sprite emoteSprite;
    public Vector3 emotePosition;
    public float emoteScale = 0.2f;

    public int stationIndex;

    // Start is called before the first frame update
    void Start()
    {
        pointList = GetComponent<PointList>();
    }

    public GameObject InstantiateItem(GameObject prefab)
    {
        Vector3 pos = GetNextPosition();
        GameObject instance = Instantiate(prefab, pos, Quaternion.identity, transform);
        items.Add(instance);

        var emote = emoteManager.InstantiateEmote<ItemPickupEmote>(instance);
        emote.position = emotePosition;
        emote.transform.localScale = new Vector3(emoteScale, emoteScale, 1);
        emote.GetComponent<Image>().sprite = emoteSprite;

        return instance;
    }

    public Vector3 GetNextPosition()
    {
        if (pointList == null)
        {
            return new Vector3(0, 0, 0);
        }

        Vector2[] points = pointList.points;
        return transform.position + (Vector3) points[items.Count % points.Length];
    }
}
