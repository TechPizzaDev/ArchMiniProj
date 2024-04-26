using System;
using UnityEngine;

public delegate void EmoteDelegate(EmoteEntry entry, bool userAction);

[Serializable]
public class EmoteEntry
{
    public GameObject Source { get; }

    public GameObject Entity { get; set; }

    public event EmoteDelegate OnClick;
    public event EmoteDelegate OnClose;

    public EmoteEntry(GameObject source)
    {
        Source = source != null ? source : throw new ArgumentNullException(nameof(source));
    }

    public void Click(bool userAction)
    {
        OnClick?.Invoke(this, userAction);
    }

    public void Close(bool userAction)
    {
        OnClose?.Invoke(this, userAction);
    }
}