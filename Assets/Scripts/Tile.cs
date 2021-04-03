using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Empty,
    Wall,
    City,
    OxyGenerator
}
public class Tile : MonoBehaviour
{
    public UI2DSprite Sprite;
    public TileType TileType;
}
