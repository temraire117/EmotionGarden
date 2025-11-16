using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] answerButtons;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private QuizLogic quizLogic;
    [SerializeField] private GameObject finalScorePanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI rewardPointsText;

    private Question currentQuestion;

    void Start()
    {
        ShowNextQuestion();
        UpdateScore();
    }

    public void OnAnswerSelected(int index)
    {
        bool isCorrect = quizLogic.CheckAnswer(index);
        string tip = currentQuestion.GetTip();
        string correctAnswer = currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());

        questionText.text = isCorrect
            ? $"정답!\n{tip}"
            : $"정답은 '{correctAnswer}' 입니다.\n\n{tip}";

        UpdateScore();
        SetButtonState(false);
    }

    public void ShowNextQuestion()
    {
        if (!quizLogic.HasMoreQuestions)
        {
            int finalScore = quizLogic.GetScore();
            int rewardPoints = finalScore;

            questionText.text = "퀴즈가 모두 끝났습니다!";
            nextButton.SetActive(false);

            ResultUI.ShowResult(finalScorePanel, finalScoreText, rewardPointsText, finalScore, rewardPoints);

            return;
        }

        currentQuestion = quizLogic.GetNextQuestion();
        DisplayQuestion(currentQuestion);
        SetButtonState(true);
    }

    void DisplayQuestion(Question question)
    {
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI btnText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = question.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        foreach (GameObject btn in answerButtons)
            btn.GetComponent<Button>().interactable = state;

        nextButton.GetComponent<Button>().interactable = !state;
    }

    void UpdateScore()
    {
        scoreText.text = $"점수 {quizLogic.GetScore()}";
    }

}
