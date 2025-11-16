using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private CollectManager collectManager;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private GameObject positiveObj;
    [SerializeField] private GameObject negativeObj;
    [SerializeField] private WordData wordData;
    [SerializeField] private RectTransform playerRect;
    [SerializeField] private float spawnInterval = 1f;

    private float timer = 0f;
    private List<FallingObject> activeObjects = new List<FallingObject>();
    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnWord();
            timer = 0f;
        }

        CheckCollisions();
    }

    void SpawnWord()
    {
        bool isPositive = Random.value > 0.5f;
        GameObject prefab = isPositive ? positiveObj : negativeObj;

        GameObject wordObj = Instantiate(prefab, canvasRect);
        RectTransform wordRect = wordObj.GetComponent<RectTransform>();

        float minX = -canvasRect.sizeDelta.x / 2 + wordRect.sizeDelta.x / 2;
        float maxX = canvasRect.sizeDelta.x / 2 - wordRect.sizeDelta.x / 2;
        float randomX = Random.Range(minX, maxX);
        wordRect.anchoredPosition =
            new Vector2(randomX, canvasRect.sizeDelta.y / 2 + wordRect.sizeDelta.y / 2);

        string word = isPositive
                    ? wordData.PositiveWords[Random.Range(0, wordData.PositiveWords.Count)]
                    : wordData.NegativeWords[Random.Range(0, wordData.NegativeWords.Count)];

        wordObj.GetComponentInChildren<TMP_Text>().text = word;

        FallingObject falling = wordObj.GetComponent<FallingObject>();
        falling.type = isPositive ? WordType.Positive : WordType.Negative;

        activeObjects.Add(falling);
    }

    void CheckCollisions()
    {
        if (isGameOver || activeObjects.Count == 0) return;

        List<FallingObject> toRemove = new List<FallingObject>();

        for (int i = 0; i < activeObjects.Count; i++)
        {
            var obj = activeObjects[i];
            if (obj == null || obj.IsOverlapping(playerRect))
            {
                if (obj != null)
                {
                    if (obj.type == WordType.Positive)
                        collectManager.AddScore();
                    else
                        collectManager.ReduceLife();

                    obj.DestroyObj();
                }
                toRemove.Add(obj);
            }
        }

        foreach (var obj in toRemove)
            activeObjects.Remove(obj);
    }
        
    public List<FallingObject> GetActiveObjects()
    {
        return activeObjects;
    }

    public void ClearActiveObjects()
    {
        activeObjects.Clear();
    }

        public void StopSpawner()
    {
        isGameOver = true;
        foreach (var obj in activeObjects)
            if (obj != null)
                obj.DestroyObj();

        activeObjects.Clear();
    }
}
