using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Tear
{
    public string name;
    public TearType type;
    public Sprite panalSprite;
    public Sprite markSprite;
}

[CreateAssetMenu(fileName = "TearSO", menuName = "ScriptableObject/TearSO")]
public class TearSO : ScriptableObject
{
    public Tear[] tears;
}
