using UnityEngine;
using UnityEngine.UI;

public class SurveySubmitHandler : MonoBehaviour
{
    [SerializeField] private Button submitButton;
    [SerializeField] private GameObject warningPopup;
    private SurveyManager surveyManager;

    void Awake()
    {
        surveyManager = FindObjectOfType<SurveyManager>();
        submitButton.onClick.AddListener(OnSubmitClicked);
    }

    public void OnSubmitClicked()
    {
        int[] results = surveyManager.GetResults();
        foreach (var r in results)
        {
            if (r == -1)
            {
                ShowWarning("모든 질문을 선택해야 제출할 수 있습니다!");
                return;
            }
        }

        int totalScore = surveyManager.GetTotalScore();
        string category = surveyManager.GetSeverityCategory();

        string today = System.DateTime.Now.ToString("yyyy-MM-dd");

        Debug.Log($"총점: {totalScore}, 범주: {category}");

        var launcher = FindObjectOfType<EmotionDiaryLauncher>();
        if (launcher != null)
        {
            launcher.OnSurveySubmit(category);
        }
    }

        private void ShowWarning(string message)
    {
        warningPopup.SetActive(true);
        var text = warningPopup.GetComponentInChildren<Text>();
        if (text != null) text.text = message;

        Invoke(nameof(HideWarning), 1.5f);
    }
        private void HideWarning()
    {
        warningPopup.SetActive(false);
    }

}
