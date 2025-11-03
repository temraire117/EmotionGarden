using UnityEngine;

[CreateAssetMenu(menuName ="Quiz Question", fileName ="New Question")]
public class Question : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] string tip = "Enter new tip";
    [SerializeField] int correctIndex;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public string GetTip()
    {
        return tip;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctIndex;
    }

}
