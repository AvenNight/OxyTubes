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
    
    private UIPanel dragPanel;

    public Action OnTilePut;

    public void Start()
    {
        dd.OnStartDrag += OnStartDrag;
        dd.OnEndDrag += OnEndDrag;
        dd.OnProcessDrag += OnProcessDrag;
    }

    private void OnStartDrag()
    {
        Position = transform.position;
        var tile = GetTile();
        if (tile != null)
        {
            tile.isEnable = true;
        }
        // возвышаем выше всего
        dragPanel = this.gameObject.AddComponent<UIPanel>();
        dragPanel.depth = 1000;
    }

    private void OnEndDrag()
    {
        SetDropPosition();
        transform.position = Position;
        if (curLightTile != null)
        {
            curLightTile.TileUnlight();
            curLightTile = null;
        }
        // возвращаем высоту
        Destroy(dragPanel);
    }

    private void SetDropPosition()
    {
        var tile = GetTile();
        if (tile == null) return;
        if (tile.TileType == TileType.Empty && tile.isEnable)
        {
            Position = tile.transform.position;
            tile.isEnable = false;
            OnTilePut?.Invoke();
        }
    }

    private Tile GetTile()
    { 
        var mousePos = Input.mousePosition;
        var mousePosWorld = camera.ScreenToWorldPoint(mousePos);
        var hit = Physics2D.Raycast(mousePosWorld, Vector2.zero);
        return !hit.collider ? null : hit.collider.GetComponent<Tile>();
    }

    private void OnProcessDrag()
    {
        var tile = GetTile();
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
