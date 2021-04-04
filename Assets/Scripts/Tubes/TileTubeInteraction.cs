using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileTubeInteraction : MonoBehaviour
{
    private UIDragDropItem dd => GetComponent<UIDragDropItem>();
    private UI2DSprite sprite => GetComponent<UI2DSprite>();
    private Camera camera => CameraController.Camera;
    
    private Vector3 Position;

    private Tile curLightTile = null;
    private Tile curTile = null;
    
    private UIPanel dragPanel;

    public Action<TileTubeInteraction> OnTilePut;
    public bool moveOnScroll { get; private set; }

    public void Start()
    {
        dd.OnStartDrag += OnStartDrag;
        dd.OnEndDrag += OnEndDrag;
        dd.OnProcessDrag += OnProcessDrag;
    }

    private void OnStartDrag()
    {
        Position = transform.position;
        if (curTile != null)
        {
            curTile.isEnable = true;
        }
        // возвышаем выше всего
        dragPanel = this.gameObject.AddComponent<UIPanel>();
        dragPanel.depth = 1000;
    }

    private void OnEndDrag()
    {
        SetDropPosition();
        var shift = Position - transform.position;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(MoveTileTube(shift));
        OnTilePut?.Invoke(this);
        if (curLightTile != null)
        {
            curLightTile.TileUnlight();
            curLightTile = null;
        }
        // возвращаем высоту
        Destroy(dragPanel);
    }

    private IEnumerator MoveTileTube(Vector3 shift, float time = 0.2f)
    {
        var curTime = 0f;

        var start = transform.position;
        var end = Position;

        while (curTime < time)
        {
            transform.position = Vector3.Lerp(start, end, curTime / time);
            curTime += Time.deltaTime;
            
            yield return 0;
        }
        if (curTime >= time)
        {
            transform.position = end;
            GetComponent<Collider>().enabled = true;
            if (moveOnScroll)
            {
                SceneController.Instance.UITileScroll.AddTube(this.GetComponent<TileTube>());

                moveOnScroll = false;

            }
        }
        
    }
    private void SetDropPosition()
    {
        var collider = GetTile();
        Tile tile = null;
        if (collider != null)
        {
            tile = collider.GetComponent<Tile>();
        }
        
        if (collider == null)
        {
            if (curTile != null)
            {
                curTile.isEnable = false;
            }
            else
            {
                moveOnScroll = true;
            }
        }
        else if (collider != null && tile == null)
        {
            curTile = null;
            Position = collider.transform.position;
            Position.y = camera.ScreenToWorldPoint(Input.mousePosition).y;
            moveOnScroll = true;
        }
        else if (tile != null)
        {
            if (tile.TileType == TileType.Empty && tile.isEnable)
            {
                Position = tile.transform.position;
                tile.isEnable = false;
                curTile = tile;
            } else if (!tile.isEnable && curTile != null)
            {
                curTile.isEnable = false;
            } else if ((tile.TileType != TileType.Empty || !tile.isEnable) && curTile == null)
            {
                moveOnScroll = true;
            }
        }
    }

    private Collider2D GetTile()
    { 
        var mousePos = Input.mousePosition;
        var mousePosWorld = camera.ScreenToWorldPoint(mousePos);
        var hit = Physics2D.Raycast(mousePosWorld, Vector2.zero);
        return !hit.collider ? null : hit.collider;
    }

    private void OnProcessDrag()
    {
        var collider = GetTile();
        Tile tile = null;
        if (collider != null)
        {
            tile = collider.GetComponent<Tile>();
        }
        
        if (tile == null )
        {
            if (curLightTile == null)
            {
                return;
            }
            curLightTile.TileUnlight();
            curLightTile = null;
            return;
        }

        if (tile != curLightTile && curLightTile != null)
        {
            curLightTile.TileUnlight();
            curLightTile = null;
        }
        if (tile.TileType == TileType.Empty && tile.isEnable)
        {
            curLightTile = tile;
            curLightTile.TileLight();
        }
    }
}
