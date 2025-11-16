using UnityEngine;

public class SurveyManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private QuestionItem questionItemPrefab;
    [SerializeField] private SelfDiagnosisQ[] questionList;

    private QuestionItem[] instantiatedItems;

    void Start()
    {
        instantiatedItems = new QuestionItem[questionList.Length];

        for (int i = 0; i < questionList.Length; i++)
        {
            var item = Instantiate(questionItemPrefab, container);
            item.Init(questionList[i]);
            instantiatedItems[i] = item;
        }
    }

    public int[] GetResults()
    {
        int[] results = new int[instantiatedItems.Length];
        for (int i = 0; i < instantiatedItems.Length; i++)
            results[i] = instantiatedItems[i].GetSelectedIndex();
        return results;
    }

    public int GetTotalScore()
    {
        int total = 0;
        foreach (var score in GetResults())
            total += score;
        return total;
    }

    public string GetSeverityCategory()
    {
        int total = GetTotalScore();
        if (total >= 1 && total <= 4) return "good";
        if (total >= 5 && total <= 9) return "mild";
        if (total >= 10 && total <= 19) return "moderate";
        if (total >= 20 && total <= 27) return "severe";
        return "unknown";
    }
}
