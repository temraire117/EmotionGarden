using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenFlower : MonoBehaviour
{
    [SerializeField] Sprite EmptySprite;
    [SerializeField] GardenUI gardenUI;
    private Image image;
    private bool isEmpty = true;

    void Start()
    {
        image = GetComponent<Image>();
        if (isEmpty)
        {
            image.sprite = EmptySprite;
        }
    }

    void Update()
    {
        
    }

    public void SelectFlower()
    {
        gardenUI.OpenSelectPannel(this);
    }

    public void SetFlower(int itemId)
    {
        image.sprite = Resources.Load<Sprite>($"Garden/{itemId}");
        isEmpty = false;
    }
}
