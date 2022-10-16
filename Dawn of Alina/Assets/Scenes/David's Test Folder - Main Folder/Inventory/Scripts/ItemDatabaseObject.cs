using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Item Database")]

public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public Item[] Items;
    public Dictionary<Item,int> GetId = new Dictionary<Item,int>();


    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<Item, int>();
        for(int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
        }
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}
