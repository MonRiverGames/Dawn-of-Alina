using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Scriptable Objects/Item")]
public class ItemObject : ScriptableObject
{
    public string Name;
    public Sprite sprite;
    [TextArea(15,20)]
    public string description;
}


