using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BiomeScript
{
    public string biomeName;
    public Color biomeColor;
    [Header("Sprites")]
    public TilesList tilesList;
    [Header("World attributes")]
    public int tree_chance = 10;
    public float terrainFreq = 0.04f;
    public float heightMultip = 40f;
    public float heightAdd = 25f;
    public int treeAdd = 10;
    public float stone_height = 25f;
    [Header("Noise Textures")]
    public float coalSize = 0.06f;
    public float goldSize = 0.06f;
    public float diamondSize = 0.06f;
    public float copperSize = 0.06f;
    public float ironSize = 0.06f;
    public float caveFreq = 0.06f;
    public Texture2D noiseTexture;
    public float coalFreaquency;
    public Texture2D coalTexture;
    public float goldFreaquency;
    public Texture2D goldTexture;
    public float ironFreaquency;
    public Texture2D ironTexture;
    public float diamondFreaquency;
    public Texture2D diamondTexture;
    public float copperFreaquency;
    public Texture2D copperTexture;

}
