using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LongNote : Note
{
    [SerializeField] private float requiredHoldTime = 3.6f;
    [SerializeField] private float stepTime = 1.8f;
    [SerializeField] private int noteScore = 5;
    private float holdTimer = 0f;
    private float scoreTimer = 0f;

    private bool inJudgeZone = false;
    private bool isFinalJudged = false;
    private bool isFirstStage = true;

     void Start()
    {
        float noteLength = fallSpeed * requiredHoldTime;

        Vector3 scale = transform.localScale;
        scale.y = noteLength;
        transform.localScale = scale;

        Vector3 pos = transform.position;
        pos.y = transform.position.y + noteLength / 2f;
        transform.position = pos;

    }

    protected override void Update()
    {
        base.Update();
        if (inJudgeZone)
        {
            var player = FindObjectOfType<PlayerState>();
            if (player != null && player.isSmiling)
            {
                holdTimer += Time.deltaTime;
                scoreTimer += Time.deltaTime;

                if(isFirstStage){
                    Judge(true, noteScore, false);
                    isFirstStage = false;
                }

                else if (!isFinalJudged && holdTimer >= requiredHoldTime)
                {
                    isFinalJudged = true;
                    Judge(true, noteScore, true);
                    Destroy(gameObject);
                }
                else if (!isFinalJudged && scoreTimer >= stepTime)
                {
                    scoreTimer = 0f;
                    Judge(true, noteScore, false);
                }

            }
            else
            {
                holdTimer = 0f;
                scoreTimer = 0f;
            }
        }

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JudgeLine"))
        {
            inJudgeZone = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (!isFinalJudged  && holdTimer < requiredHoldTime)
        {
            isFinalJudged = true;
            Judge(false, 0, true);
            Destroy(gameObject);
        }
    }

    public float GetrequiredHoldTime() => requiredHoldTime;
    public void SetRequireHoldTime(float time)
    {
        stepTime = time/2;        
        requiredHoldTime = time;
    }
}
