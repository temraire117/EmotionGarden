using UnityEngine;

[CreateAssetMenu(menuName = "ItemBook/FlowerItem")]
public class FlowerItem : ScriptableObject
{
    public string flowerName;
    public Sprite flowerImage;
    [TextArea] public string flowerMeaning;
}
