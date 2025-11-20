using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClosetSlotUI : MonoBehaviour
{
    [SerializeField] private Image ItemImage;
    [SerializeField] private Button equipButton;

    private int itemId;
    private string nameText;

    private ClosetUI closet;
    private string itemType;
    private bool isEquipped = false;

    public void Initialize(ClosetUIData data, ClosetUI closetRef)
    {
        closet = closetRef;

        itemId = data.itemId;
        nameText = data.name;
        itemType = data.type;
        ItemImage.sprite = data.sprite;
        isEquipped = data.isEquipped;

        equipButton.onClick.AddListener(() =>
        {
            closet.EquipItem(itemId, itemType, this);
        });
    }

    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
    }


    public int GetItemId() => itemId;

    public string GetNameText() => nameText;
    public string GetItemType() => itemType;
}
