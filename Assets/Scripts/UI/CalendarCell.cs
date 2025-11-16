using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalendarCell : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public Image emojiImage;


    // 인덱스 순서: 0 = good, 1 = mild . . .
    public Sprite[] emojiSprites;

    public void SetEmpty()
    {
        dateText.text = "";
        emojiImage.enabled = false;
    }

    public void SetDate(int day, string fullDate)
    {
        dateText.text = day.ToString();

        var record = DatabaseController.Instance.GetDiary(fullDate);

        if (record != null && !string.IsNullOrEmpty(record.mood))
        {
            int idx = MoodToIndex(record.mood);
            if (idx >= 0 && idx < emojiSprites.Length && emojiSprites[idx] != null)
            {
                emojiImage.sprite = emojiSprites[idx];
                emojiImage.enabled = true;
            }
            else
            {
                emojiImage.enabled = false;
            }
        }
        else
        {
            emojiImage.enabled = false;
        }
    }

    int MoodToIndex(string mood)
    {
        if (string.IsNullOrEmpty(mood)) return -1;
        string m = mood.Trim().ToLowerInvariant();

        switch (m)
        {
            case "good":
                return 0;
            case "mild":
                return 1;
            case "moderate":
                return 2;
            case "severe":
                return 3;
            default:
                return -1;
        }
    }
}
