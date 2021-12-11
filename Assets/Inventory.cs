using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventory", menuName = "New inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemDef> items = new List<ItemDef>(8);
    public TilesList tiles;
    void OnValidate()
    {
        items = new List<ItemDef>(8);
    }
    public void AddItem(string name)
    {
        if (tiles.inventoryItems.Exists((el) => el.name == name))
        {
            var find = items.FindLastIndex((el) => el.name == name);
            var inventory_examp = tiles.inventoryItems.Find((el) => el.name == name);
            if (find != -1)
            {
                if (inventory_examp.is_stackable && inventory_examp.max_count >= items[find].count + 1)
                {
                    items[find].count++;
                }
                else if (items.Count <= 8)
                {
                    items.Add(new ItemDef(inventory_examp.sprite, inventory_examp.name, 1));
                }
            }
            else if (items.Count <= 8)
            {
                items.Add(new ItemDef(inventory_examp.sprite, inventory_examp.name, 1));
            }
        }
    }
    public void RemoveSingleItem(string name)
    {
        if (tiles.inventoryItems.Exists((el) => el.name == name))
        {
            var find = items.FindLastIndex((el) => el.name == name);
            var inventory_examp = tiles.inventoryItems.Find((el) => el.name == name);
            if (find != -1)
            {
                if (inventory_examp.is_stackable && items[find].count > 1)
                {
                    items[find].count--;
                }
                else if (items.Count <= 1)
                {
                    items.RemoveAt(find);
                }
            }
        }
    }
    public int RemoveSingleItemAt(int index)
    {
        try
        {
            if (tiles.inventoryItems.Count > index && index >= 0)
            {
                var inventory_examp = tiles.inventoryItems.Find((el) => el.name == items[index].name);
                if (inventory_examp.is_stackable && items[index].count > 1)
                {
                    items[index].count--;
                }
                else if (items.Count <= 1)
                {
                    items.RemoveAt(index);
                }
                return 1;
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
            return 0;
        }
        return 0;
    }
}
public class ItemDef : ScriptableObject
{
    public Sprite sprite;
    public string name;
    public int count;
    public ItemDef(Sprite sprite, string name, int count)
    {
        this.sprite = sprite;
        this.name = name;
        this.count = count;
    }
}
