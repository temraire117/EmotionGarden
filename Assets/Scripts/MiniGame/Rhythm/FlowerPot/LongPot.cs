using UnityEngine;
using UnityEngine.UI;

public class LongPot : FlowerPot
{
    //0: 화분, 1: 새싹, 2: 봉오리, 3: 꽃 
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
        if(stage < 3)
        {
            potImage.sprite = growingSprites[++stage];
            return;
        }

    }

}


