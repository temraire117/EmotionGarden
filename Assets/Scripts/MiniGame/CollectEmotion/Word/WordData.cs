using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/WordData", fileName ="WordData")]
public class WordData : ScriptableObject
{
    [SerializeField] private List<string> positiveWords;
    [SerializeField] private List<string> negativeWords;

    public List<string> PositiveWords => positiveWords;
    public List<string> NegativeWords => negativeWords;

}
