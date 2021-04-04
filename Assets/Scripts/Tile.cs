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
    public bool isEnable = true;

    public (int X, int Y) Coords { get; protected set; }
    public virtual void Set(int x, int y)
    {
        Coords = (x, y);

        //switch (type)
        switch (TileType)
        {
            case TileType.Empty:
            default:
                Sprite.sprite2D = ArtCollection.Instance.Empty;
                break;
            case TileType.Wall:
                Sprite.sprite2D = ArtCollection.Instance.GetRandomWall();
                break;
            case TileType.City:
            case TileType.OxyGenerator:
                // do nothing
                break;
        }
    }

    public void TileLight()
    {
        Sprite.color = Color.blue;
    }

    public void TileUnlight()
    {
        Sprite.color = Color.white;
    }
}
