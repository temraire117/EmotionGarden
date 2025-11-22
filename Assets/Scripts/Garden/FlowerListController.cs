using System.Collections.Generic;
using UnityEngine;

public class FlowerListController : MonoBehaviour
{
    [SerializeField] private Transform listParent;
    [SerializeField] private GameObject flowerItemPrefab;
    [SerializeField] private float spacing = 130f;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        ClearList();

        List<Item> myFlowers = DatabaseController.Instance.GetMyItemsByType("flower");

        float yOffset = 0f;

        foreach (var flower in myFlowers)
        {
            GameObject obj = Instantiate(flowerItemPrefab, listParent, false);
            FlowerItemUI ui = obj.GetComponent<FlowerItemUI>();
            ui.SetItem(flower);

            RectTransform rt = obj.GetComponent<RectTransform>();
            float prefabHalfHeight = rt.sizeDelta.y / 2f;

            yOffset -= prefabHalfHeight;

            rt.anchoredPosition = new Vector2(0f, yOffset);

            yOffset -= (prefabHalfHeight + spacing);
        }

        RectTransform contentRT = listParent.GetComponent<RectTransform>();
        contentRT.sizeDelta = new Vector2(contentRT.sizeDelta.x, -yOffset);
    }

    private void ClearList()
    {
        for (int i = listParent.childCount - 1; i >= 0; i--)
            Destroy(listParent.GetChild(i).gameObject);
    }
}
