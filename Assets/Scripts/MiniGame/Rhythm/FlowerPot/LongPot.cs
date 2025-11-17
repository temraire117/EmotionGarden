using UnityEngine;

public class LongPot : FlowerPot
{
    //0: 화분, 1: 새싹, 2: 봉오리, 3: 꽃 
    [SerializeField] private Sprite[] growingSprites;
    private Sprite sprite;
    private int stage = 0;

    void Start()
    {
        SpriteRenderer potSpriteRenderer = GetComponent<SpriteRenderer>();
        float potLength = potSpriteRenderer.bounds.size.y;
        sprite = potSpriteRenderer.sprite;

        Vector3 pos = transform.position;
        pos.y += potLength / 2f;
        transform.position = pos;
    }

    public override void bloom()
    {
        if(stage < 3)
        {
            sprite = growingSprites[++stage];
            return;
        }

    }

}


