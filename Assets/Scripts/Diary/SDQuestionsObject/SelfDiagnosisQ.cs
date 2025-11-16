using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfDiagnosis", menuName = "Survey/SelfDiagnosisQ")]
public class SelfDiagnosisQ : ScriptableObject
{
    [TextArea(2, 6)]
    public string questionText;

    public string[] options = 
    {
        "없음",
        "2-3일 이상",
        "7일 이상",
        "거의 매일"
    };
}
