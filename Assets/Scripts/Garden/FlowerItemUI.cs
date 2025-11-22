using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FlowerItemUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] Image icon;

    private int itemId;

    public void SetItem(Item item)
    {
        nameText.text = item.name;
        itemId = item.item_id;
        icon.sprite = Resources.Load<Sprite>($"Flowers/{item.item_id}");
    }

    public void OnButtonClick()
    {
        GardenUI gardenUI = FindObjectOfType<GardenUI>();
        gardenUI.OnFlowerSelected(itemId);        
    }
}
