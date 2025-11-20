
using TMPro;
using UnityEngine;

public class ClosetUI : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private ClosetSlotUI slotPrefab;
    [SerializeField] private bool isEquipped = false;
    [SerializeField] private CharacterEquip characterEquip;
    private string ItemTag = "hair";
    private int currentEquippedItemId = -1;

    void Start()
    {
        LoadItems(ItemTag);
    }

    void Update()
    {

    }


    void LoadItems(string itemTag)
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
    
        var ownedItems = DatabaseController.Instance.GetMyItemsByType(itemTag);

        foreach (var item in ownedItems)
        {
            var slot = Instantiate(slotPrefab, gridParent);

            Sprite sprite = LoadItemSprite(item.item_id);

            var data = new ClosetUIData(item.item_id, item.name, sprite, item.type);

            slot.Initialize(data, this);
        }
    }

    private Sprite LoadItemSprite(int itemId)
    {
        return Resources.Load<Sprite>($"Styles/{itemId}");
    }

    public void SetItemTag(string tag)
    {
        LoadItems(tag);
    }

    public void EquipItem(int itemId, string itemType, ClosetSlotUI slot)
    {
        int partIndex = GetPartIndex(itemType);
        if (partIndex == -1)
        {
            Debug.LogError($"typeError");
            return;
        }

        if (currentEquippedItemId == itemId)
        {
            characterEquip.UnequipItem(partIndex);
            slot.SetEquipped(false);
            currentEquippedItemId = -1;
            return;
        }
        
        if (currentEquippedItemId != -1)
        {
            foreach (ClosetSlotUI childSlot in gridParent.GetComponentsInChildren<ClosetSlotUI>())
            {
                if (childSlot.GetItemId() == currentEquippedItemId)
                {
                    childSlot.SetEquipped(false);
                    break;
                }
            }
        }

        currentEquippedItemId = itemId;
        slot.SetEquipped(true);

        GameObject prefab = Resources.Load<GameObject>($"ItemPrefabs/{itemId}");
        characterEquip.EquipItem(prefab, partIndex);

    }

    private int GetPartIndex(string type)
    {
        return type switch
        {
            "hair" => 0,
            "top" => 1,
            "bottom" => 2,
            "shoes" => 3,
            _ => -1
        };
    }
        
}
