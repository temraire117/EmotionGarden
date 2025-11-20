using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBookController : MonoBehaviour
{
    // === UI & Prefab 연결 ===
    public Image itemIcon;
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public Button PrevButton;
    public Button NextButton;
    // === 현재 아이템 ID ===
    private int currentItemId = 101; // 시작 아이템 ID
    private const int firstItemId = 101; // 첫 아이템 ID

    void Start()
    {
        if (DatabaseController.Instance == null)
        {
            Debug.LogError("DatabaseController.Instance is null! DatabaseController 오브젝트가 씬에 있는지 확인하세요.");
            return;
        }
        Debug.Log("DatabaseController.Instance 정상적으로 존재합니다.");
        PrevButton.onClick.AddListener(OnPrevButton);
        NextButton.onClick.AddListener(OnNextButton);
        ShowCurrentItem();
    }

    // === 현재 아이템을 DB에서 가져와 UI에 세팅 ===
    private void ShowCurrentItem()
    {
            // 모든 DB 아이템 출력
        var allItems = DatabaseController.Instance.GetFlowerItems();
        Debug.Log("DB에 있는 모든 아이템:");
        foreach (var i in allItems)
            Debug.Log($"Item ID: {i.item_id}, Name: {i.name}");

        // 현재 페이지 아이템 확인
        var currentItem = DatabaseController.Instance.GetMyItemById(currentItemId);
        Debug.Log(currentItem != null ? $"Current Item: {currentItem.name}" : $"Item with ID {currentItemId} not found");

        var item = DatabaseController.Instance.GetMyItemById(currentItemId);
        if (item != null)
        {
            Setup(item);
        }
        else
        {    
            Debug.LogWarning($"Item with ID {currentItemId} not found.");
            // 아이템 없으면 UI 비우기 가능
            itemName.text = "???";
            itemDescription.text = "";
            itemIcon.sprite = null;
        }
    }

    // === Prev 버튼 클릭 ===
    public void OnPrevButton()
    {
        if (currentItemId > firstItemId)
        {
            currentItemId -= 1;
            ShowCurrentItem();
        }
    }

    // === Next 버튼 클릭 ===
    public void OnNextButton()
    {
        currentItemId += 1;
        ShowCurrentItem();
    }

    // === DB Item으로 UI 세팅 ===
    private void Setup(Item item)
    {
        itemName.text = item.name;
        itemDescription.text = item.flowers_lang;
        itemIcon.sprite = GetItemSprite(item.item_id);
    }

    // === item_id 기준으로 Resources에서 Sprite 로드 ===
    private Sprite GetItemSprite(int itemId)
    {
        var item = DatabaseController.Instance.GetMyItemById(itemId);
        if (item == null)
        {
            Debug.LogWarning($"Item {itemId} not found in DB");
            return null;
        }

        return Resources.Load<Sprite>($"Items/{item.item_id}");
    }
}
