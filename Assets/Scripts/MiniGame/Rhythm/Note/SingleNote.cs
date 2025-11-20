using UnityEngine;

public class SingleNote : Note
{
    [SerializeField] private int noteScore = 5;
    private bool isFinalJudged = false;
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
        if (other.CompareTag("JudgeLine") && !isFinalJudged)
        {
            if (player != null && player.isSmiling)
            {
                Debug.Log("Good");
                isFinalJudged = true;
                Judge(true, noteScore, isFinalJudged);
                Destroy(gameObject);
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("JudgeLine") && !isFinalJudged)
        {
            isFinalJudged = true;
            Judge(false, 0, isFinalJudged);
            Destroy(gameObject);
        }
    }
}
