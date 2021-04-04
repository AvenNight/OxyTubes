using UnityEngine;

public class TileData
{
    public bool Up { get; protected set; }
    public bool Down { get; protected set; }
    public bool Left { get; protected set; }
    public bool Right { get; protected set; }
}

public class TileTube : MonoBehaviour
{
    public UISprite Sprite;
    public TileData Data { get; protected set; }

    public void Set(TileData data)
    {
        Data = data;


    }
}
