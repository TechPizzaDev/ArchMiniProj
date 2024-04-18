using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

#nullable enable

public class EmoteManager : MonoBehaviour
{
    [SerializeField]
    private List<EmoteEntry> emotes = new();

    public GameObject EmoteTemplate;

    void Start()
    {

    }

    void Update()
    {
        foreach (EmoteEntry entry in emotes)
        {
            if (entry.Source.IsDestroyed())
            {
                entry.Close(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            EmoteEntry? entry = GetEntryAt(Input.mousePosition);
            if (entry != null)
            {
                entry.Click(true);
            }
        }
    }

    public void InstantiateEmote(EmoteEntry entry)
    {
        entry.Entity = Instantiate(EmoteTemplate, entry.Source.transform);
        entry.Entity.transform.localPosition = new Vector3(0, 1, 0);

        AddEmote(entry);
    }

    public void AddEmote(EmoteEntry entry)
    {
        entry.OnClose += Entry_OnClose;
        emotes.Add(entry);
    }

    private void Entry_OnClose(EmoteEntry entry, bool userAction)
    {
        if (!emotes.Remove(entry))
        {
            Debug.LogError("Entry closed while not added to manager", entry.Source);
            return;
        }
        entry.OnClose -= Entry_OnClose;

        Destroy(entry.Entity);
    }

    public EmoteEntry? GetEntryAt(Vector3 position)
    {
        RaycastHit2D hit = RayHelper.RaycastFromCamera(position);

        Collider2D collider = hit.collider;
        if (collider == null)
        {
            return null;
        }

        GameObject obj = collider.gameObject;
        foreach (EmoteEntry entry in emotes)
        {
            if (entry.Entity == obj)
            {
                return entry;
            }
        }
        return null;
    }
}
