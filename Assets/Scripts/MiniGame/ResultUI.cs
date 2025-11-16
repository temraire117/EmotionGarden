using UnityEngine;
using TMPro;

public static class ResultUI
{
    public static void ShowResult(GameObject finalScorePanel, 
                                  TextMeshProUGUI finalScoreText, 
                                  TextMeshProUGUI rewardPointsText, 
                                  int finalScore, 
                                  int rewardPoints)
    {
        finalScoreText.text = $"최종 점수: {finalScore}점";
        rewardPointsText.text = $"획득한 감정포인트: {rewardPoints}포인트";

        UIManager.Instance.OpenModal(finalScorePanel);

        GameManager.Instance.AddEmotionPoints(rewardPoints);
    }
}
