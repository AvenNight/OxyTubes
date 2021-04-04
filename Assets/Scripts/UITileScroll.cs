using GameCore.UI.Layout;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UITileScroll : MonoBehaviour
{
    [SerializeField] protected VerticalLayout layout;
    [SerializeField] protected UIScrollView scrollView;

    public TileTube TileTubePrefab;

    //protected List<Tile> Tiles;
    protected List<TileTube> Tiles;
    //protected int step => Tiles.FirstOrDefault().Sprite.height;

    //public void Set(List<TileType> tiles)
    public void Set(List<TileData> tiles)
    {
        Clear();

        Tiles = new List<TileTube>();

        foreach (var tile in tiles)
        {
            var o = CreateTile(tile);
            var ddTube = o.GetComponent<TileTubeInteraction>();
            ddTube.OnTilePut += () =>
            {
                o.transform.parent = this.transform;
                UpdateLayout();
            };
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

    protected TileTube CreateTile(TileData data)
    {
        var result = Instantiate(TileTubePrefab.gameObject, layout.transform).GetComponent<TileTube>();

        switch (data.Sides)
        {
            //case 1:
            //    result.Sprite.sprite2D = ArtCollection.Instance.Tube1;
            //    break;
            //case 2:
            //    result.Sprite.sprite2D = ArtCollection.Instance.Tube2Line;
            //    break;
            //case 3:
            //    result.Sprite.sprite2D = ArtCollection.Instance.Tube3;
            //    break;
            //case 4:
            //    result.Sprite.sprite2D = ArtCollection.Instance.Tube4;
            //    break;
            default:
                //return null;
                break;
        }

        //return Instantiate(TileTubePrefab.gameObject, layout.transform).GetComponent<TileTube>();
        return result;
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
