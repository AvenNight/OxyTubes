using GameCore.UI.Layout;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITileScroll : MonoBehaviour
{
    [SerializeField] protected VerticalLayout layout;
    [SerializeField] protected UIScrollView scrollView;

    public TileTube TileTubePrefab;

    protected List<TileTube> Tiles;
    //protected int step => Tiles.FirstOrDefault().Sprite.height;

    public void Set(List<TubeData> tiles)
    {
        Clear();

        Tiles = new List<TileTube>();

        foreach (var tile in tiles)
        {
            var o = CreateTile(tile);
            var ddTube = o.GetComponent<TileTubeInteraction>();
            ddTube.OnTilePut += (cursorTube, underTile) =>
            {
                if (!cursorTube.moveOnScroll)
                    o.transform.parent = this.transform;
                UpdateLayout();

                if (underTile == null)
                    return;

                var curTube = cursorTube.GetComponent<TileTube>();
                SceneController.Instance.LevelTubeData[underTile.Coords.X, underTile.Coords.Y] = curTube.Data;

                var check = SceneController.Instance.LevelCheck();
                Debug.Log(check);
            };

            ddTube.OnTilePick += (cursorTube, underTile) =>
            {
                if (underTile == null)
                    return;

                //var curTube = cursorTube.GetComponent<TileTube>();
                SceneController.Instance.LevelTubeData[underTile.Coords.X, underTile.Coords.Y] = null;
            };

            Tiles.Add(o);
        }

        UpdateLayout();
    }

    public void OnArrowUp() => ScrollMove(true);
    public void OnArrowDown() => ScrollMove(false);

    public void AddTube(TileTube tile)
    {
        Tiles.Add(tile);
        tile.transform.parent = layout.transform;
        UpdateLayout();
    }

    protected void ScrollMove(bool up)
    {
        //UpdateScroll();
        //layout.transform.localPosition += (up ? step : -step) * Vector3.up;
        StartCoroutine(ScrollMoveProccess((up ? -150 : 150) * Vector3.up));
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

    protected TileTube CreateTile(TubeData data)
    {
        var result = Instantiate(TileTubePrefab.gameObject, layout.transform).GetComponent<TileTube>();
        result.Set(data);
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
