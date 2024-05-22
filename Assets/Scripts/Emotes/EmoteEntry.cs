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

    protected virtual void InvokeClick(PointerEventData? eventData)
    {
        OnClick?.Invoke(this, eventData);
    }

    public void Click(PointerEventData? eventData)
    {
        InvokeClick(eventData);

        if (destroyOnClick)
        {
            Destroy(gameObject);
        }
    }

    public void Close(PointerEventData? eventData)
    {
        OnClose?.Invoke(this, eventData);
    }

    void Awake()
    {
    }

    protected virtual void Start()
    {
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

            transform.position = CalculatePosition();
        }
    }

    public virtual Vector3 CalculatePosition()
    {
        Vector3 targetPosition = position;

        if (followSource && Source != null)
        {
            targetPosition += Source.transform.position;
        }

        return targetPosition;
    }
}