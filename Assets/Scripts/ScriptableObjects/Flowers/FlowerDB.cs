using UnityEngine;

[CreateAssetMenu(menuName = "ItemBook/FlowerDatabase")]
public class FlowerDatabase : ScriptableObject
{
    public FlowerItem[] flowers; // 도감에 표시할 모든 꽃 데이터
}
