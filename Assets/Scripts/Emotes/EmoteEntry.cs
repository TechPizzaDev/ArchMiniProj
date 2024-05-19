using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable enable

public delegate void EmoteDelegate(EmoteEntry entry, PointerEventData? eventData);

public class EmoteEntry : MonoBehaviour, IPointerClickHandler
{
    public bool followSource = true;
    public bool destroyOnClick = true;

    public Vector3 position;

    public GameObject? Source { get; set; }

    public event EmoteDelegate? OnClick;
    public event EmoteDelegate? OnClose;

    public virtual bool IsClickingEvent(PointerEventData eventData)
    {
        return eventData.button == PointerEventData.InputButton.Left;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (IsClickingEvent(eventData))
        {
            Click(eventData);
        }
    }

    public void Click(PointerEventData? eventData)
    {
        OnClick?.Invoke(this, eventData);

        if (destroyOnClick)
        {
            Destroy(gameObject);
        }
    }

    public void Close(PointerEventData? eventData)
    {
        OnClose?.Invoke(this, eventData);
    }

    protected virtual void Start()
    {
        UpdatePosition();
    }

    void Update()
    {
        if (followSource)
        {
            if (Source.IsDestroyed())
            {
                Destroy(gameObject);
                return;
            }

            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        Vector3 targetPosition = position;

        if (followSource && Source != null)
        {
            targetPosition += Source.transform.position;
        }

        transform.position = targetPosition;
    }
}