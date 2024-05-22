using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable enable

public delegate void EmoteDelegate(EmoteEntry entry, PointerEventData? eventData);

public class EmoteEntry : MonoBehaviour, IPointerClickHandler
{
    private Image? _image;

    public bool followSource = true;
    public bool destroyOnClick = true;

    public Vector3 position;
    public float jiggleRate = 5f;
    public float jiggleFactor = 0.04f;

    public GameObject? Source { get; set; }

    public event EmoteDelegate? OnClick;
    public event EmoteDelegate? OnClose;

    public Image GetImage()
    {
        if (_image == null)
            _image = GetComponent<Image>();
        return _image;
    }

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

        if (destroyOnClick)
        {
            Destroy(gameObject);
        }
    }

    public void Click(PointerEventData? eventData)
    {
        InvokeClick(eventData);
    }

    protected virtual void InvokeClose(PointerEventData? eventData)
    {
        OnClose?.Invoke(this, eventData);

        Destroy(gameObject);
    }

    public void Close(PointerEventData? eventData)
    {
        InvokeClose(eventData);
    }

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
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

        if (jiggleFactor != 0)
        {
            targetPosition += new Vector3(0, Mathf.Sin(Time.time * jiggleRate) * jiggleFactor, 0);
        }

        return targetPosition;
    }
}