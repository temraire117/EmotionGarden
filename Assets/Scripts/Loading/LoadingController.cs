using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI percent;
    [SerializeField] Tips tipList;
    [SerializeField] TextMeshProUGUI tip;
    [SerializeField] SceneButtonManager sceneManager;

    void Start()
    {
        int rand = Random.Range(0, tipList.TipList.Count);
        tip.text = tipList.TipList[rand];
        StartCoroutine(PercentAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    IEnumerator PercentAnimation()
    {
        float duration = 1.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float value = Mathf.Lerp(0f, 100f, t / duration);
            percent.text = Mathf.RoundToInt(value) + "%";
            yield return null;
        }

        percent.text = "100%";

        sceneManager.LoadScene(SceneChanger.NextScene);
    }
}
