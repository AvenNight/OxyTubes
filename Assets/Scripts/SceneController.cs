using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public SceneCreator SceneCreator;
    public UITileScroll UITileScroll;

    private void OnEnable()
    {
        //sceneCreator.CreateScene(Levels.Level1);
        SceneCreator.CreateScene(Levels.Level2);

        var tiles = new List<TileType>
        {
            TileType.Empty,
            TileType.Wall,
            TileType.Empty,
            TileType.Wall,
            TileType.Empty,
            TileType.Wall,
            TileType.Empty,
            TileType.Wall,
            TileType.Empty,
            TileType.Wall,
            TileType.Empty,
            TileType.Wall,
            TileType.Empty,
            TileType.Wall,
            TileType.Empty,
         };

        UITileScroll.Set(tiles);
    }

    public void OnClick(Tile tile)
    {
        Debug.Log($"{tile.TileType}");
    }
}
