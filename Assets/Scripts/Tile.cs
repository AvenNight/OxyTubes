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
    //public Vector2 Position;

    
}
