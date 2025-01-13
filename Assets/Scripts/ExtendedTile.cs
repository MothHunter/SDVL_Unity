using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExtendedTile : Tile
{
    public bool isBuildable = false;
    public bool isTillable = false;
    public bool allowCrops = false;
    public bool allowPlants = false;
    public bool isWater = false;
}
