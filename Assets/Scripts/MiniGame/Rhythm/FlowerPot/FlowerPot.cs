using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPot : MonoBehaviour
{
    [SerializeField] protected float fallSpeed = 6f;
    private RectTransform rect;
    private RectTransform canvasRect;
    private RectTransform targetRect;
    private bool isFalling = true;
    
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
        if (!isFalling || targetRect == null) return;

        rect.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

        if (IsOverlapping(targetRect))
        {
            isFalling = false;
        }
        
    }

    public virtual void bloom(){}

    public bool IsOverlapping(RectTransform deskRect)
    {
        return RectOverlaps(rect, deskRect);
    }
    protected bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Rect aRect = new Rect(a.anchoredPosition - a.sizeDelta / 2, a.sizeDelta);
        Rect bRect = new Rect(b.anchoredPosition - b.sizeDelta / 2, b.sizeDelta);
        return aRect.Overlaps(bRect);
    }

    public void SetTargetRect(RectTransform target)
    {
        targetRect = target;
    }

    public void DestroyObj()
    {
        StartCoroutine(MoveRightAndDestroy());
    }

    private IEnumerator MoveRightAndDestroy()
    {
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            rect.anchoredPosition += Vector2.right * fallSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    public bool IsFalling => isFalling;

}
