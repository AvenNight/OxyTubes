using UnityEngine;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    public SceneCreator SceneCreator;
    public UITileScroll UITileScroll;

    private void OnEnable()
    {
        //sceneCreator.CreateScene(Levels.Level1);
        SceneCreator.CreateScene(Levels.Level3);

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
