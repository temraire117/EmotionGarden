using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Tips", fileName ="Tip List")]
public class Tips : ScriptableObject
{
    [SerializeField] private List<string> tipList;
    public List<string> TipList => tipList;

}
