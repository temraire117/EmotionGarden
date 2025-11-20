using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip sfxClip;
    [SerializeField] private List<GameObject> lifeImage;

    private int score = 0;
    private int life = 3;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore()
    {
        score += 10;
        sfxSource.PlayOneShot(sfxClip);
        UpdateUI();
    }

    public void ReduceLife()
    {
        life -= 1;
        UpdateUI();
        for(int i = 0; i< lifeImage.Count; i++)
        {
            lifeImage[i].SetActive(i < life);
        }
        if (life <= 0)
        {
            life = 0;
            EndGame();
        }
    }

    void UpdateUI()
    {
        scoreText.text = $"점수 {score}";
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
