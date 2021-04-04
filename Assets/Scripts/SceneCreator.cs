using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    public Transform MapRoot;
    public Vector2 MapOffset;

    public Tile Empty;
    public Tile Wall;

    public void CreateScene(char [,] map)
    {
        var curPos = Vector2.zero;

        Tile tile = null;

        for (int x = map.GetLength(0) - 1; x >= 0; x--)
        {
            curPos.x = 0;
            for (int y = 0; y < map.GetLength(1); y++)
            {
                tile = CreateTile(map[x, y]);
                tile.transform.localPosition = curPos;
                curPos.x += tile.Sprite.width;
            }
            curPos.y += tile.Sprite.height;
        }

        MapRoot.localPosition = -curPos / 2 + MapOffset;
    }

    protected Tile CreateTile(char type)
    {
        switch (type)
        {
            case ' ':
            default:
                return Instantiate(Empty.gameObject, MapRoot).GetComponent<Tile>();
            case 'w':
                return Instantiate(Wall.gameObject, MapRoot).GetComponent<Tile>();
        }
    }
}
