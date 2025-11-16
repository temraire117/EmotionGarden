using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionDiaryLauncher : MonoBehaviour
{
    public GameObject calendarPanel;
    public GameObject surveyPanel; 

        private void OnEnable()
    {
        OnOpenEmotionStorage();
    }

    public void OnOpenEmotionStorage()
    {
        if (DatabaseController.Instance.HasDiaryToday())
        {
            surveyPanel.SetActive(false);
            calendarPanel.SetActive(true);
        }
        else
        {
            surveyPanel.SetActive(true);
            calendarPanel.SetActive(false);
        }
    }

    public void OnSurveySubmit(string mood)
    {
        string today = System.DateTime.Now.ToString("yyyy-MM-dd");
        DatabaseController.Instance.SaveDiary(today, mood);

        surveyPanel.SetActive(false);
        calendarPanel.SetActive(true);

        var cal = FindObjectOfType<CalendarUI>();
        if (cal != null) cal.UpdateCalendar();
    }
}
