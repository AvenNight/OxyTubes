using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    public Transform MapRoot;

    public Tile Empty;
    public Tile Wall;

    public void CreateScene(char [,] map)
    {
        var curPos = new Vector2(-1000 , -400);

        Tile tile = null;

        for (int x = map.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                tile = CreateTile(map[x, y]);
                tile.transform.localPosition = curPos;
                curPos.x += tile.Sprite.width;
            }
            curPos.x = -1000;
            curPos.y += tile.Sprite.height;
        }
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
