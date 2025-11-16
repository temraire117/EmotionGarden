using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public Color normalColor = Color.white;
    public Color smilingColor = Color.yellow;

    private bool useCameraDetection = false; //나중에 카메라 연결 시
    public bool isSmiling;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        isSmiling = CheckSmile();
        spriteRenderer.color = isSmiling ? smilingColor : normalColor;
    }
    
    bool CheckSmile()
    {
        if (useCameraDetection)
            return false;
            //return FaceDetector.IsSmiling(); // 추후 표정 인식 이용
        else
        {
            // PC
            if (Input.GetMouseButton(0))
                return true;

            // 모바일
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || 
                    touch.phase == TouchPhase.Moved || 
                    touch.phase == TouchPhase.Stationary)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
