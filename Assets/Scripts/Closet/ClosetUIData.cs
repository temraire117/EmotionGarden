using UnityEngine;

public class ClosetUIData
{
    public int itemId;
    public string name;
    public Sprite sprite;
    public bool isEquipped;
    public string type;

    public ClosetUIData(int itemId, string name, Sprite sprite = null, string type = "", bool isEquipped = false)
    {
        this.itemId = itemId;
        this.name = name;
        this.sprite = sprite;
        this.type = type;
        this.isEquipped = isEquipped;
    }
}
