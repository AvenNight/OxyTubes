using System;
using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(UIDragDropItem))]
public class TileTube : MonoBehaviour
{
    private Vector2 Position;
    private static Vector2 ZeroPos = new Vector2(-1000 , -400);
    private static Vector2 ScrollPos = new Vector2(700 , 200);
    private static Vector2 ScrollBoundsMin = new Vector2(500 , -500);
    private static Vector2 ScrollBoundsMax = new Vector2(900 , 500);
    //public bool onScroll = true;
    
    

    private UIDragDropItem dd => GetComponent<UIDragDropItem>();

    public void Start()
    {
        dd.OnStartDrag += OnStartDrag;
        dd.OnEndDrag += OnEndDrag;
        dd.OnEndDrag += OnProcessDrag;
        Position = ScrollPos;
    }

    private void OnStartDrag()
    {
        //Debug.Log("Start drag");
        PickTile();
    }

    private void OnEndDrag()
    {
        //Debug.Log("End drag");
        DropTile();
    }
    private void OnProcessDrag()
    {
        //if (CanPlace())
        {
            
        }
        //else
        {
            
        }
    }

    private void DropTile()
    {
        Vector2 position = transform.localPosition;
        var zeroPos = ZeroPos;
        var sprite = GetComponent<UI2DSprite>();
        var tileSize = new Vector2(sprite.width,sprite.height);
        //var tileSize = new Vector2(100,100);
        var mapSize = new Vector2(Levels.Level3.GetLength(0), Levels.Level3.GetLength(1));
        position.x += tileSize.x / 2;
        position.y += tileSize.y / 2;
        if (position.x > zeroPos.x && position.x < zeroPos.x + mapSize.x * tileSize.x &&
            position.y > zeroPos.y && position.y < zeroPos.y + mapSize.y * tileSize.y)
        {
            var mapPosition = new Vector2Int((int)((position.x - zeroPos.x)/ tileSize.x), (int) ((position.y - zeroPos.y)/ tileSize.y));
            if (Levels.Level3[4 - mapPosition.y, mapPosition.x].Equals(' '))
            {
                Position = new Vector2(zeroPos.x + tileSize.x * mapPosition.x, zeroPos.y + tileSize.y * mapPosition.y);
            }
        } else if (position.x > ScrollBoundsMin.x && position.x < ScrollBoundsMax.x &&
                   position.y > ScrollBoundsMin.y && position.y < ScrollBoundsMax.y)
        {
            Position = ScrollPos;
        }

        transform.localPosition = Position;
    }
    private void PickTile()
    {
        
    }
}
