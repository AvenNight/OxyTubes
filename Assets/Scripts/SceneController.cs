using UnityEngine;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    public SceneCreator SceneCreator;
    public UITileScroll UITileScroll;
    public Timer Timer;

    private void OnEnable()
    {
        SceneCreator.CreateScene(Levels.Level1);
        //SceneCreator.CreateScene(Levels.Level3);

        //var tiles = new List<TileType>
        //{
        //    TileType.Empty,
        //    TileType.Wall,
        //    TileType.Empty,
        //    TileType.Wall,
        //    TileType.Empty,
        //    TileType.Wall,
        //    TileType.Empty,
        //    TileType.Wall,
        //    TileType.Empty,
        //    TileType.Wall,
        //    TileType.Empty,
        //    TileType.Wall,
        //    TileType.Empty,
        //    TileType.Wall,
        //    TileType.Empty,
        // };

        UITileScroll.Set(Levels.GetTilesData(Levels.Level1Tubes));
        Timer.Set(120f, 0.1f);
    }

    public void OnClick(Tile tile)
    {
        Debug.Log($"{tile.TileType}");
    }
}
