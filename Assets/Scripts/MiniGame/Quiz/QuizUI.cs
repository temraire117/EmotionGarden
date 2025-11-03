using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<Question> questions = new List<Question>();
    [SerializeField] Question currentQuestion;
    [SerializeField] GameObject[] answerButtons;

    private int correctAnswerIndex;
    private int cnt = 0;


    void Start()
    {
        GetNextQuestion();
    }

    public void OnAnswerSelected(int index)
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        string tip = currentQuestion.GetTip();
        if (index == correctAnswerIndex)
        {
            questionText.text = "정답!" + $"\n{tip}";
        }
        else
        {
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = $"정답은 '{correctAnswer}' 입니다" + $"\n\n{tip}";
        }

        SetButtonState(false);
    }
        public void GetNextQuestion()
    {
        if(questions.Count>0 && cnt < 5)
        {
            SetButtonState(true);
            GetRandomQuestion();
            DisplayQuestions();
            cnt++;
        }
    }
    
    void DisplayQuestions()
    {
        TextMeshProUGUI buttonText;
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < 4; i++)
        {
            buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
            questions.Remove(currentQuestion);
        
    }
}
