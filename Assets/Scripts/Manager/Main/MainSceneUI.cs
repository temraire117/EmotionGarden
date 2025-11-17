using TMPro;
using UnityEngine;


public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    private int lastPoints = -1;

    void Start()
    {
        UpdatePointsUI();
    }

    void Update()
    {
        int currentPoints = GameManager.Instance.EmotionPoints;
        if (currentPoints != lastPoints)
        {
            UpdatePointsUI();
            lastPoints = currentPoints;
        }
    }

    void UpdatePointsUI()
    {
        pointsText.text = $"감정포인트: {GameManager.Instance.EmotionPoints}";
    }

    public void onOpenModalButtonClicked(GameObject modal)
    {
        UIManager.Instance.OpenModal(modal);
    }

    public void onCloseModalButtonClicked(GameObject modal)
    {
        UIManager.Instance.CloseModal(modal);
    }

}
