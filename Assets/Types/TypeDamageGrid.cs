using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeDamageGrid
{
	public List<TypeDataScriptable> TypeDataCollection = new List<TypeDataScriptable>();
	public int size;

	public void GenerateGridData (List<TypeDataScriptable> typeDataCollection)
    {
		TypeDataCollection = typeDataCollection;

	}
}
