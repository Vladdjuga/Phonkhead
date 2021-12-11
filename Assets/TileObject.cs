using Assets.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileObject : ScriptableObject
{
    [SerializeField]
    public string sprite;
    [SerializeField]
    public bool is_breakable;
    [SerializeField]
    public bool is_bg;
    [SerializeField]
    public int def;
    [SerializeField]
    public Vector3 position;
    public TileObject(TileObjectHelper helper)
    {
        this.sprite = helper.sprite;
        this.is_breakable = helper.is_breakable;
        this.is_bg = helper.is_bg;
        this.def = helper.def;
        this.position = new Vector3(helper.position.x, helper.position.y, helper.position.z);
    }

    public TileObject()
    {
    }
}
