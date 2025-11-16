using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] optionButtons;

    private int selectedIndex = -1;

    public void Init(SelfDiagnosisQ data)
    {
        questionText.text = data.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            var btn = optionButtons[i];

            
            btn.GetComponentInChildren<TextMeshProUGUI>().text = data.options[i];

            
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                SelectOption(idx);
            });
        }

        UpdateButtonVisuals();
    }

    private void SelectOption(int index)
    {
        selectedIndex = index;
        UpdateButtonVisuals();
    }

    private void UpdateButtonVisuals()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            var btn = optionButtons[i];
            var img = btn.GetComponent<Image>();
            var txt = btn.GetComponentInChildren<TextMeshProUGUI>();

            if (i == selectedIndex)
            {
                img.color = Color.black;
                txt.color = Color.white;
            }
            else
            {
                img.color = Color.white;
                txt.color = Color.black;
            }
        }
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
}
