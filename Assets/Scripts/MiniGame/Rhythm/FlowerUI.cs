using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerUI : MonoBehaviour
{
    [SerializeField] GameObject singleFlowerPot;
    [SerializeField] GameObject longFlowerPot;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform targetPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPot(bool isLong)
    {
        GameObject prefab = isLong ? longFlowerPot : singleFlowerPot;
        GameObject potObj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        
    }

}
