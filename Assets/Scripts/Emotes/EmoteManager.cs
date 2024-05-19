using UnityEngine;

#nullable enable

public class EmoteManager : MonoBehaviour
{
    public Canvas worldCanvas;

    public GameObject EmoteTemplate;

    public Vector3 emotePosition;

    void Start()
    {
        worldCanvas = GameObject.FindGameObjectWithTag("WorldCanvas").GetComponent<Canvas>();
    }

    public T InstantiateEmote<T>(GameObject? source)
        where T : EmoteEntry
    {
        GameObject obj = Instantiate(EmoteTemplate, worldCanvas.transform);

        T entry = obj.AddComponent<T>();
        entry.Source = source;

        entry.position = emotePosition;

        return entry;
    }
}
