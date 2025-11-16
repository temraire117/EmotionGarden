using UnityEngine;

public enum WordType { Positive, Negative }

public class FallingObject : MonoBehaviour
{
    public WordType type;
    [SerializeField] private float fallSpeed = 200f;

    private RectTransform rect;
    private RectTransform canvasRect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

        if (rect.anchoredPosition.y < -rect.parent.GetComponent<RectTransform>().sizeDelta.y / 2 - rect.sizeDelta.y)
        {
            DestroyObj();
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }

    public bool IsOverlapping(RectTransform playerRect)
    {
        return RectOverlaps(rect, playerRect);
    }
    
    private bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Rect aRect = new Rect(a.anchoredPosition - a.sizeDelta / 2, a.sizeDelta);
        Rect bRect = new Rect(b.anchoredPosition - b.sizeDelta / 2, b.sizeDelta);
        return aRect.Overlaps(bRect);
    }
}
