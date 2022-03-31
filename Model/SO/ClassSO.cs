using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdventurerClass
{
    public string name;
    public ClassType classType;
    public Sprite sprite;
}


[CreateAssetMenu(fileName = "ClassSO", menuName = "ScriptableObject/ClassSO")]
public class ClassSO : ScriptableObject
{
    public AdventurerClass[] AdventurerClasses;
}
