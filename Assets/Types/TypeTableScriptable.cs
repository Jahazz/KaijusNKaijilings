using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypeDataTable", menuName = "ScriptableObjects/TypeDataTable", order = 2)]
public class TypeTableScriptable : ScriptableObject
{
    [field: SerializeField]
    public TypeDamageGrid DamageGrid { get; private set; }
    [field: SerializeField]
    public List<TypeDataScriptable> TypesCollection { get; set; }

    private void OnValidate ()
    {
        DamageGrid.GenerateGridData(TypesCollection);
        GenerateTypeBoundIfNotExists();
    }

    private void GenerateTypeBoundIfNotExists ()
    {
        foreach (var item in TypesCollection)
        {
            foreach (var item2 in TypesCollection)
            {
                AddConnectionIfNotExists(item.AttackerMultiplierCollection, item2);
            }
        }
    }

    private void AddConnectionIfNotExists (List<TypeDamagePair> itemToGenerateConnection, TypeDataScriptable connectionToLookFor)
    {
        if (DoesListContainsBound(itemToGenerateConnection, connectionToLookFor) == false)
        {
            itemToGenerateConnection.Add(new TypeDamagePair(connectionToLookFor, 0));
        }
    }

    private bool DoesListContainsBound (List<TypeDamagePair> list, TypeDataScriptable connectedType)
    {
        bool output = false;

        foreach (TypeDamagePair item in list)
        {
            if (item.TypeData == connectedType)
            {
                output = true;
            }
        }

        return output;
    }
}
