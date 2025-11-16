using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private float minX = -400f;
    [SerializeField] private float maxX = 400f;
    [SerializeField] private float moveSpeed = 10f;

    private RectTransform playerRect;
    private Vector2 targetPos;
    void Start()
    {
        playerRect = GetComponent<RectTransform>();
        targetPos = playerRect.anchoredPosition;
    }

    void Update()
    {
        Vector2 localPoint;

        // 모바일
        if (Input.touchCount > 0)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, Input.GetTouch(0).position, null, out localPoint
            );
            targetPos = new Vector2(localPoint.x, playerRect.anchoredPosition.y);
        }
        // PC 테스트
        else if (Input.GetMouseButton(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, Input.mousePosition, null, out localPoint
            );
            targetPos = new Vector2(localPoint.x, playerRect.anchoredPosition.y);
        }

        Vector2 pos = Vector2.Lerp(playerRect.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        playerRect.anchoredPosition = pos;
    }
}
