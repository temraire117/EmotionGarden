using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBookController : MonoBehaviour
{
    [Header("Book Data")]
    public FlowerDatabase database; // FlowerDatabase.asset 연결

    [Header("UI")]
    public Image itemIcon;
    public TMP_Text itemName;
    public TMP_Text description;

    [Header("Page Button")]
    public Button prevButton;
    public Button nextButton;

    int currentPage = 0;

    void Start()
    {
        if (database == null || database.flowers.Length == 0) return;

        UpdatePage();
        
        // 버튼 이벤트 연결
        prevButton.onClick.AddListener(PrevPage);
        nextButton.onClick.AddListener(NextPage);
    }

    void UpdatePage()
    {
        if (database == null || database.flowers.Length == 0) return;

        var data = database.flowers[currentPage];

        itemIcon.sprite = data.flowerImage;
        itemName.text = data.flowerName;
        description.text = data.flowerMeaning;

        // 페이지 끝 처리
        prevButton.interactable = currentPage > 0;
        nextButton.interactable = currentPage < database.flowers.Length - 1;
    }

    void PrevPage()
    {
        if (currentPage <= 0) return;
        currentPage--;
        UpdatePage();
    }

    void NextPage()
    {
        if (currentPage >= database.flowers.Length - 1) return;
        currentPage++;
        UpdatePage();
    }
}
