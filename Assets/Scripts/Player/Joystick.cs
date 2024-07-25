using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform handle;
    public RectTransform outLine;

    private float deadZone = 0;
    private float handleRange = 1;
    private Vector2 input = Vector2.zero;
    private Canvas canvas;

    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }

    void Start()
    {
        canvas = GameObject.Find("UI_Touch").GetComponent<Canvas>();
        outLine = gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(outLine, eventData.position, canvas.worldCamera, out localPoint))
        {
            Vector2 radius = outLine.sizeDelta / 2;
            input = localPoint / (radius * canvas.scaleFactor);
            HandleInput(input.magnitude, input.normalized);
            handle.anchoredPosition = input * radius * handleRange;
        }
        //input = (eventData.position - outLine.anchoredPosition) / (radius * canvas.scaleFactor);
    }

    private void HandleInput(float magnitude, Vector2 normalized)
    {
        if (magnitude > deadZone)
        {
            input = normalized;
        }
        else
        {
            input = Vector2.zero;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
