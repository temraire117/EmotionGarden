using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EmotionGarden.Models;

public class StyleShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Transform gridParent;
    [SerializeField] private StyleSlotUI slotPrefab;
    [SerializeField] private Confirm confirmModal;
    private int lastPoints = -1;
    private string ItemTag = "hair";

    void Start()
    {
        UpdatePointsUI();
        LoadItems(ItemTag);
    }

    void Update()
    {
        int currentPoints = GameManager.Instance.EmotionPoints;
        if (currentPoints != lastPoints)
        {
            UpdatePointsUI();
            lastPoints = currentPoints;
        }
    }

    void UpdatePointsUI()
    {
        pointsText.text = $"{GameManager.Instance.EmotionPoints}";
    }

    void LoadItems(string itemTag)
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
        
        List<ItemIdPriceName> Items = DatabaseController.Instance.GetItemsByType(itemTag);
        var ownedItems = DatabaseController.Instance.GetMyItemsByType(itemTag);

        foreach (var item in Items)
        {
            var slot = Instantiate(slotPrefab, gridParent);

            bool isOwned = ownedItems.Exists(i => i.item_id == item.item_id);

            Sprite sprite = LoadItemSprite(item.item_id);

            var data = new StyleUIData(item.item_id, item.name, item.price, sprite);

            slot.Initialize(data, this, isOwned, confirmModal);
        }
    }

    private Sprite LoadItemSprite(int itemId)
    {
        return Resources.Load<Sprite>($"Styles/{itemId}");
    }

    public void TryBuyItem(int itemId, int price)
    {
        int points = GameManager.Instance.EmotionPoints;

        if (points < price)
        {
            Debug.Log("포인트 부족!");
            return;
        }

        
        var myItems = DatabaseController.Instance.GetMyItems();
        if (myItems.Exists(i => i.item_id == itemId))
        {
            Debug.Log("이미 구매한 아이템입니다.");
            return;
        }

        DatabaseController.Instance.AddItem(itemId);
        GameManager.Instance.UseEmotionPoints(price);

        UpdatePointsUI();
        Debug.Log($"구매 완료: {itemId}");
    }

    public void SetItemTag(string tag)
    {
        LoadItems(tag);
    }
    
}
