using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerUI : MonoBehaviour
{
    [SerializeField] GameObject singleFlowerPot;
    [SerializeField] GameObject longFlowerPot;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] RectTransform spawnPoint;
    [SerializeField] RectTransform targetPoint;
    [SerializeField] JudgeController judgeController;

    [SerializeField] AudioSource sfxSource;
    // 0: drop , 1: blooming , 2: bloom, 3: miss
    [SerializeField] List<AudioClip> sounds;

    private FlowerPot currentPot;
    private bool hasPlayed = false;

    void Start()
    {
        judgeController.OnJudged += potBloom;
    }

    void Update()
    {
        if (!currentPot.IsFalling && !hasPlayed)
        {
            sfxSource.PlayOneShot(sounds[0]);
            hasPlayed = true;
        }

        if (currentPot.IsFalling)
        {
            hasPlayed = false;
        }
    }

    public void SpawnPot(bool isLong)
    {
        GameObject prefab = isLong ? longFlowerPot : singleFlowerPot;
        GameObject potObj = Instantiate(prefab, canvasRect);
        
        RectTransform potRect = potObj.GetComponent<RectTransform>();
        potRect.anchoredPosition = spawnPoint.anchoredPosition;

        FlowerPot potScript = potObj.GetComponent<FlowerPot>();
        potScript.SetTargetRect(targetPoint);

        currentPot = potScript;
    }

    public void DestroyPot()
    {
        if (currentPot != null)
        {
            currentPot.DestroyObj();
            currentPot = null;
        }
    }

    private void potBloom(bool isGood, bool isJudged)
    {
        if (isGood)
        {
            currentPot.bloom();
            if(isJudged) sfxSource.PlayOneShot(sounds[2]);
            else sfxSource.PlayOneShot(sounds[1]);
        }
        else if(!isGood && !isJudged)
        {
            return;
        }

        if (isJudged)
        {
            if(!isGood) sfxSource.PlayOneShot(sounds[3]);
            DestroyPot();
        }

    }

}
