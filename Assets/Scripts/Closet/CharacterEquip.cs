using UnityEngine;

public class CharacterEquip : MonoBehaviour
{
    // 0:Hair, 1:Top, 2:Bottom, 3:Shoes
    [SerializeField] private Transform[] equipPoints;
    private GameObject[] currentEquips = new GameObject[4];

    public void EquipItem(GameObject itemPrefab, int partIndex)
    {
        if (currentEquips[partIndex] != null)
            Destroy(currentEquips[partIndex]);

        var item = Instantiate(itemPrefab, equipPoints[partIndex]);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        currentEquips[partIndex] = item;
    }

    public void UnequipItem(int partIndex)
    {
        if (currentEquips[partIndex] != null)
        {
            Destroy(currentEquips[partIndex]);
            currentEquips[partIndex] = null;
        }
    }
}
