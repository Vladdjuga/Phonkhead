using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventoryitem", menuName = "New Item")]
public class InventoryItem : ScriptableObject
{
    public string name;
    public string code;
    public Sprite sprite;
    public bool is_stackable;
    public int max_count;
    public InventoryItem(string name,string code,Sprite sprite,bool is_stackable,int max_count)
    {
        this.sprite = sprite;
        this.name = name;
        this.code = code;
        this.is_stackable = is_stackable;
        this.max_count = max_count;
    }
    public InventoryItem(string name, string code, Sprite sprite)
    {
        this.sprite = sprite;
        this.name = name;
        this.code = code;
        this.is_stackable = false;
        this.max_count = 1;
    }
    public InventoryItem Copy()
    {
        InventoryItem inventoryItem = new InventoryItem(name,code,sprite,is_stackable,max_count);
        return inventoryItem;
    }
}
