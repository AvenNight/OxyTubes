using GameCore.UI.Layout;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UITileScroll : MonoBehaviour
{
    [SerializeField] protected VerticalLayout layout;
    [SerializeField] protected UIScrollView scrollView;

    public Tile Empty;
    public Tile Wall;

    protected List<Tile> Tiles;
    protected int step => Tiles.FirstOrDefault().Sprite.height;

    public void Set(List<TileType> tiles)
    {
        Clear();

        Tiles = new List<Tile>();

        foreach (var tile in tiles)
        {
            var o = CreateTile(tile);
            Tiles.Add(o);
        }

        UpdateLayout();
    }

    public void OnArrowUp() => ScrollMove(true);
    public void OnArrowDown() => ScrollMove(false);

    protected void ScrollMove(bool up)
    {
        //UpdateScroll();
        //layout.transform.localPosition += (up ? step : -step) * Vector3.up;
        StartCoroutine(ScrollMoveProccess((up ? 150 : -150) * Vector3.up));
    }

    protected IEnumerator ScrollMoveProccess(Vector3 shift, float time = 0.2f)
    {
        var curTime = 0f;

        var start = layout.transform.localPosition;
        var end = layout.transform.localPosition + shift;

        while (curTime < time)
        {
            layout.transform.localPosition = Vector3.Lerp(start, end, curTime / time);
            UpdateScroll();
            curTime += Time.deltaTime;
            yield return 0;
        }
    }

    protected void Clear()
    {
        if (Tiles == null) return;

        foreach (var tile in Tiles)
            DestroyImmediate(tile);

        Tiles.Clear();
    }

    protected Tile CreateTile(TileType type)
    {
        switch (type)
        {
            case TileType.Empty:
            default:
                return Instantiate(Empty.gameObject, layout.transform).GetComponent<Tile>();
            case TileType.Wall:
                return Instantiate(Wall.gameObject, layout.transform).GetComponent<Tile>();
            case TileType.City:
            case TileType.OxyGenerator:
                return null;
        }
    }

    protected void UpdateLayout()
    {
        layout.InitLayoutFromScene();
        layout.Align();
    }

    protected void UpdateScroll()
    {
        scrollView.UpdatePosition();
        scrollView.UpdateScrollbars();
        scrollView.RestrictWithinBounds(false);
    }
}
