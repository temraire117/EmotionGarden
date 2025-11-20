using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StyleSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Button buyButton;

    private int itemId;
    private int price;
    private string nameText;
    private Confirm confirmModal;

    private StyleShopUI shop;

    public void Initialize(StyleUIData data, StyleShopUI shopRef, bool isOwned, Confirm confirmPannel)
    {
        shop = shopRef;
        confirmModal = confirmPannel;

        itemId = data.itemId;
        price = data.price;
        nameText = data.name;
        priceText.text = price.ToString();
        ItemImage.sprite = data.sprite;

        if (isOwned)
        {
            priceText.text = "보유중";
            buyButton.interactable = false;
        }
        else
        {
            priceText.text = price.ToString();
            buyButton.interactable = true;
            buyButton.onClick.AddListener(() =>
            {
                confirmModal.Show($"[{nameText}]을(를) {price}포인트에 구매하시겠습니까?", () =>
                {
                    shop.TryBuyItem(itemId, price);
                    priceText.text = "보유중";
                    buyButton.interactable = false;
                });
            });
        }
    }

    public int GetItemId() => itemId;
    public int GetPrice() => price;
    public string GetNameText() => nameText;
}
