using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newtileslistclass", menuName = "New Tiles list")]
public class TilesList : ScriptableObject
{
    [Header("Tiles")]
    public TileDef earth;
    public TileDef stone;
    public TileDef grass;
    public TileDef gravel;
    public TileDef air;
    public TileDef air_side;
    public TileDef air_sideClouds;
    public TileDef air_sideHills;
    public TileDef air_top;
    public TileDef water;
    public TileDef log;
    public TileDef leaf;
    public TileDef coal;
    public TileDef gold;
    public TileDef iron;
    public TileDef diamond;
    public TileDef copper;
    public TileDef snow;
    public TileDef sand;
    public GameObject torch_prefab;
    public TileDef[] tiny_sprites;
    [Header("Inventory")]
    public List<InventoryItem> inventoryItems;

    public TileDef GetTileByName(string name)
    {
        //Debug.LogError(name);
        foreach (var item in this.GetType().GetFields())
        {
            TileDef def = item.GetValue(this) as TileDef;
            //Debug.LogError(item);
            if (def != null)
            {
                if (def.name == name)
                    return def;
            }
        }
        return earth;
    }
}
