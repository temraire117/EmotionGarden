using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBookController : MonoBehaviour
{
    // === UI & Prefab 연결 ===
    [SerializeField] private List<Image> itemIcons;
    [SerializeField] private List<TMP_Text> itemNames;
    [SerializeField] private List<TMP_Text> itemDescriptions;
    
    [SerializeField] private Button PrevButton;
    [SerializeField] private Button NextButton;

    //추가
    private List<Item> allFlowers;      // 전체 꽃 목록
    private HashSet<int> ownedFlowerIds; // 내가 가진 꽃 ID
    private int currentIndex = 0; 

    void Start()
    {
        if (DatabaseController.Instance == null)
        {
            Debug.LogError("DatabaseController.Instance is null! DatabaseController 오브젝트가 씬에 있는지 확인하세요.");
            return;
        }
        PrevButton.onClick.AddListener(OnPrevButton);
        NextButton.onClick.AddListener(OnNextButton);

        //추가
        allFlowers = DatabaseController.Instance.GetAllItemsByType("flower"); // 전체 꽃 목록
        ownedFlowerIds = new HashSet<int>(
            DatabaseController.Instance.GetMyItemsByType("flower").Select(i => i.item_id)
        );

        ShowCurrentItem();
    }

    // === 현재 아이템을 DB에서 가져와 UI에 세팅 ===
    private void ShowCurrentItem()
    {
        //수정
        int startIndex = currentIndex * 3;

        for (int i = 0; i < 3; i++)
        {
            int flowerIndex = startIndex + i;

            if (flowerIndex < allFlowers.Count)
            {
                var flower = allFlowers[flowerIndex];

                if (ownedFlowerIds.Contains(flower.item_id))
                {
                    itemNames[i].text = flower.name;
                    itemDescriptions[i].text = flower.flowers_lang;
                    itemIcons[i].sprite = Resources.Load<Sprite>($"Flowers/{flower.item_id}");
                    itemIcons[i].color = Color.white;
                }
                else
                {
                    itemNames[i].text = "???";
                    itemDescriptions[i].text = "";
                    itemIcons[i].sprite = Resources.Load<Sprite>($"Flowers/{flower.item_id}");
                    itemIcons[i].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
            }
            else
            {
                itemNames[i].text = "";
                itemDescriptions[i].text = "";
                itemIcons[i].sprite = null;
                itemIcons[i].color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

    //수정
    // === Prev 버튼 클릭 ===
    public void OnPrevButton()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowCurrentItem();
        }
    }

    //수정
    // === Next 버튼 클릭 ===
    public void OnNextButton()
    {
        if ((currentIndex + 1) * 3 < allFlowers.Count)
        {
            currentIndex++;
            ShowCurrentItem();
        }
    }
}
