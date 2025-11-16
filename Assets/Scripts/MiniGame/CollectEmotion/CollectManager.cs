using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;

    [SerializeField] private GameObject finalScorePanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI rewardPointsText;
    [SerializeField] private ObjectSpawner spawner;
    [SerializeField] private PlayerTouchController player;

    private int score = 0;
    private int life = 3;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore()
    {
        score += 10;
        UpdateUI();
    }

    public void ReduceLife()
    {
        life -= 1;
        if (life <= 0)
        {
            life = 0;
            EndGame();
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = $"점수 {score}";
        lifeText.text = $"남은 기회 {life}";
    }

    void EndGame()
    {
        int rewardPoints = score;
        ResultUI.ShowResult(finalScorePanel, finalScoreText, rewardPointsText, score, rewardPoints);

        if (player != null)
        {
            player.enabled = false;
        }

        if (spawner != null)
        {
            spawner.StopSpawner();
        }
    }
}
