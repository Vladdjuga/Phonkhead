using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileObject : ScriptableObject
{
    public string sprite;
    public bool is_breakable;
    public bool is_bg;
    public int def;
    public Vector3 position;
}
