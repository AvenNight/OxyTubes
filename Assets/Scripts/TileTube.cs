using UnityEngine;

public class TileData
{
    public bool Up { get; protected set; }
    public bool Down { get; protected set; }
    public bool Left { get; protected set; }
    public bool Right { get; protected set; }
    public int Sides => (Up ? 1 : 0) + (Down ? 1 : 0) + (Left ? 1 : 0) + (Right ? 1 : 0);

    public TileData(bool up, bool down, bool left, bool right)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;
    }
}

public class TileTube : MonoBehaviour
{
    public UI2DSprite Sprite;
    public TileData Data { get; protected set; }

    public void Set(TileData data)
    {
        Data = data;


    }
}
