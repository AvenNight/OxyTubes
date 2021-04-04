using UnityEngine;

public class Building : Tile
{
    public TubeData Data { get; protected set; }

    public void Set(TileType type, TubeData data)
    {
        Data = data;
        TileType = type;

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
                Sprite.sprite2D =
                    data.Left ? ArtCollection.Instance.CityLeft :
                    data.Right ? ArtCollection.Instance.CityRight :
                    data.Up ? ArtCollection.Instance.CityUp :
                    data.Down ? ArtCollection.Instance.CityDown : null;
                break;
            case TileType.OxyGenerator:
                Sprite.sprite2D = ArtCollection.Instance.Oxy;
                this.transform.rotation = Quaternion.Euler(0, 0,
                    data.Up ? 90 :
                    data.Down ? -90 : 0);
                if (data.Left)
                    Sprite.flip = UIBasicSprite.Flip.Horizontally;
                break;
        }
    }
}
