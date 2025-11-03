using TMPro;
using UnityEngine;


public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    void Start()
    {
        UpdatePointsUI();
    }

    void Update()
    {
        UpdatePointsUI();
    }

    void UpdatePointsUI()
    {
        pointsText.text = $"감정포인트: {GameManager.Instance.EmotionPoints}";
    }

    public void OpenModal(GameObject modal)
    {
        if (modal != null)
            modal.SetActive(true);
    }

    public void CloseModal(GameObject modal)
    {
        if (modal != null)
            modal.SetActive(false);
    }

}
