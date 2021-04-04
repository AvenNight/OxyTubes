using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    public Transform MapRoot;
    public Vector2 MapOffset;

    public Tile Empty;
    public Tile Wall;
    public Tile City;
    public Tile OxyGenerator;

    public void CreateScene(char [,] map)
    {
        var lvl = new TubeData[map.GetLength(0), map.GetLength(1)];

        MapClean();

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

                ///
                //lvl[x,y]=new TubeData()
                ///

            }
            curPos.y += tile.Sprite.height;
        }

        MapRoot.localPosition = -curPos / 2 + MapOffset;
    }

    protected void MapClean()
    {
        int n = MapRoot.transform.childCount;
        for (int i = 0; i < n; i++)
            Destroy(MapRoot.transform.GetChild(i).gameObject);
    }

    protected Tile CreateTile(char type)
    {
        switch (type)
        {
            case ' ': // свободное место
            default:
                return Instantiate(Empty.gameObject, MapRoot).GetComponent<Tile>();
            case 'w': // препядствие
                return Instantiate(Wall.gameObject, MapRoot).GetComponent<Tile>();
            case 'l': case 'r': case 'u': case 'd': // город + направление
                var city = Instantiate(City.gameObject, MapRoot).GetComponent<Building>();
                var dataC = new TubeData(type == 'u', type == 'd', type == 'l', type == 'r');
                city.Set(TileType.City, dataC);
                return city;
            case '1': case '2': case '3': case '4': // генератор + направление
                var oxy = Instantiate(OxyGenerator.gameObject, MapRoot).GetComponent<Building>();
                var dataO = new TubeData(type == '3', type == '4', type == '1', type == '2');
                oxy.Set(TileType.City, dataO);
                return oxy;
        }
    }
}
