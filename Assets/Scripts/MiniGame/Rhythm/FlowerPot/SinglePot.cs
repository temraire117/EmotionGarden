using UnityEngine;
using UnityEngine.UI;

public class SinglePot : FlowerPot
{
    //0: 화분, 1: 꽃
    [SerializeField] private Sprite[] growingSprites;
    private Image potImage;
    private int stage = 0;
    void Start()
    {
        potImage = GetComponent<Image>();
        potImage.sprite = growingSprites[0];
        
    }

    public override void bloom()
    {
        if(stage < 1)
        {
            potImage.sprite = growingSprites[++stage];
            return;
        }

    }

}


