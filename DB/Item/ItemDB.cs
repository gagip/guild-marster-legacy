using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ItemDB : ScriptableObject
{
	public List<ItemDBEntity> Item; // Replace 'EntityType' to an actual type that is serializable.
}
