using UnityEngine;

public class SingleNote : Note
{
    [SerializeField] private int noteScore = 5;
    private bool isHit = false;
    private PlayerState player;
    void Start()
    {
        float noteLength = GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 pos = transform.position;
        pos.y += noteLength / 2f;
        transform.position = pos;

        player = FindObjectOfType<PlayerState>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("JudgeLine") && !isHit)
        {
            if (player != null && player.isSmiling)
            {
                Debug.Log("Good");
                Judge(true, noteScore);
                isHit = true;
                Destroy(gameObject);
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("JudgeLine") && !isHit)
        {
            Judge(false, 0);
            Debug.Log("Miss");
        }
        base.OnTriggerExit2D(other);
    }
}
