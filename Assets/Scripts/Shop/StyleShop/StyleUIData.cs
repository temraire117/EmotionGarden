using UnityEngine;

public class StyleUIData
{
    public int itemId;
    public string name;
    public int price;
    public Sprite sprite;

    public StyleUIData(int itemId, string name, int price, Sprite sprite = null)
    {
        this.itemId = itemId;
        this.name = name;
        this.price = price;
        this.sprite = sprite;
    }
}
