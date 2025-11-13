using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JudgeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject finalScorePanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI rewardPointsText;
            
    private int score = 0;

    void Start()
    {
        UpdateUI();
    }
    public void RegisterNote(Note note)
    {
        //note객체의 OnNoteJudged 이벤트 발생 시 HandleNoteJudegd 호출
        note.OnNoteJudged += HandleNoteJudged;
        
    }

    private void HandleNoteJudged(bool isGood, int noteScore)
    {
        if (isGood)
        {
            score += noteScore;
            Debug.Log("Good!");
            UpdateUI();
        }
        else
        {
            Debug.Log("Miss!");
        }
    }

    void UpdateUI()
    {
        scoreText.text = $"점수 {score}";
    }

    public void EndGame()
    {
        int rewardPoints = score;
        ResultUI.ShowResult(finalScorePanel, finalScoreText, rewardPointsText, score, rewardPoints);
    }
}
