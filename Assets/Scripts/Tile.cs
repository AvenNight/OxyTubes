using System;
using UnityEngine;

public enum TileType
{
    Empty,
    Wall,
    City,
    OxyGenerator
}

[RequireComponent(typeof(UIDragDropItem))]
public class Tile : MonoBehaviour
{
    public UI2DSprite Sprite;
    public TileType TileType;

    private UIDragDropItem dd => this.GetComponent<UIDragDropItem>();

    public void Start()
    {
        dd.OnStartDrag += OnStartDrag;
        dd.OnEndDrag += OnEndDrag;
    }

    private void OnStartDrag()
    {
        Debug.Log("Start drag");
    }

    private void OnEndDrag()
    {
        Debug.Log("End drag");
    }
}
