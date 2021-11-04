using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newtileslistclass", menuName = "New Tiles list")]
public class TilesList : ScriptableObject
{
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
    public GameObject torch_prefab;
    public TileDef[] tiny_sprites;
}
