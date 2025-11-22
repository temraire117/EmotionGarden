using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenUI : MonoBehaviour
{
    [SerializeField] GameObject selectFlowerPannel;
    [SerializeField] FlowerListController flowerList;
    private GardenFlower selectedFlower;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenSelectPannel(GardenFlower flower)
    {
        selectedFlower = flower;
        selectFlowerPannel.SetActive(true);
    }

    public void CloseSelectPannel()
    {
        selectFlowerPannel.SetActive(false);
    }

    public void OnFlowerSelected(int itemId)
    {
        if(selectedFlower != null)
        {
            selectedFlower.SetFlower(itemId);
        }
    }
}
