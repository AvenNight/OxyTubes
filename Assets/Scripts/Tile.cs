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

    private void Start()
    {
        Set(TileType);
    }

    private void Set(TileType type)
    {
        switch (type)
        {
            case TileType.Empty:
            default:
                Sprite.sprite2D = ArtCollection.Instance.Empty;
                break;
            case TileType.Wall:
                Sprite.sprite2D = ArtCollection.Instance.GetRandomWall();
                break;
            case TileType.City:
                break;
            case TileType.OxyGenerator:
                break;
        }
    }
}
