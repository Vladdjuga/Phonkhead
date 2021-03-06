using Assets.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class TileMap
{
    [SerializeField]
    public TileObject[][] tiles;
    public TileMap(int x,int y)
    {
        tiles = new TileObject[x][];
        for (int i = 0; i < x; i++)
        {
            tiles[i] = new TileObject[y];
        }
    }
    public TileMap(TileMapHelper helper)
    {
        tiles = new TileObject[helper.tiles.Length][];
        for (int i = 0; i < helper.tiles.Length; i++)
        {
            tiles[i] = new TileObject[helper.tiles[i].Length];
            for (int j = 0; j < helper.tiles[i].Length; j++)
            {
                tiles[i][j] = new TileObject(helper.tiles[i][j]);
            }
        }
    }
    public void addTile(string sprite_name,bool is_breakable,bool is_bg,int def, Vector3 position,int x ,int y)
    {
        TileObject tileObject = TileObject.CreateInstance<TileObject>();
        tileObject.sprite = sprite_name;
        tileObject.is_breakable = is_breakable;
        tileObject.is_bg = is_bg;
        tileObject.def = def;
        tileObject.position = position;
        tiles[x][y] = tileObject;
    }
}

