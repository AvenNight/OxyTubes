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
    
    private Vector3 deltaPos;
    private Vector3 pickPosition;
    private Vector3 Position;

    public void Start()
    {
        dd.OnStartDrag += OnStartDrag;
        dd.OnEndDrag += OnEndDrag;
        dd.OnProcessDrag += OnProcessDrag;
    }

    private void OnStartDrag()
    {
        var mousePos = Input.mousePosition;
        var mousePosWorld = camera.ScreenToWorldPoint(mousePos);
        pickPosition = transform.position;
        var tilePos = transform.localPosition;
        tilePos.x += sprite.width;
        tilePos.y += sprite.height;
        var tilePosWorld = camera.ScreenToWorldPoint(tilePos);
        deltaPos = mousePosWorld - tilePosWorld;
    }

    private void OnEndDrag()
    {
        SetDropPosition();
        transform.position = Position;
    }

    private void SetDropPosition()
    { 
        var mousePos = Input.mousePosition;
        var mousePosWorld = camera.ScreenToWorldPoint(mousePos);
        var position = mousePosWorld - deltaPos;
        var hit = Physics2D.Raycast(mousePosWorld, Vector2.zero);
        if (!hit.collider)
        {
            //Debug.Log("no collider");
            return;
        }
        var tile = hit.collider.GetComponent<Tile>();
        if (tile == null)
        {
            return;
        }

        if (tile.TileType == TileType.Empty)
        {
            Position = tile.transform.position;
        }
    }

    private void OnProcessDrag()
    {
    }

    private void Update()
    {
    }
}
