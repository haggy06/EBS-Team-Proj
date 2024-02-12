using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ProjectExcel : ScriptableObject
{
    public List<DB_Text> Text; // Replace 'EntityType' to an actual type that is serializable.
    // public List<DB_Interaction> Interaction; // Replace 'EntityType' to an actual type that is serializable.
	public List<DB_Event> Event; // Replace 'EntityType' to an actual type that is serializable.
}
