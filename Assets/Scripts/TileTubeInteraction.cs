using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTubeInteraction : MonoBehaviour
{
    //public UI2DSprite Sprite;

    private Vector2 Position;
    private UIDragDropItem dd => GetComponent<UIDragDropItem>();

    private UIPanel dragPanel;
    public void Start()
    {
        dd.OnStartDrag += OnStartDrag;
        dd.OnEndDrag += OnEndDrag;
        dd.OnEndDrag += OnProcessDrag;
        Position = Levels.ScrollPos;
    }

    private void OnStartDrag()
    {
        //Debug.Log("Start drag");
        PickTile();
        dragPanel = this.gameObject.AddComponent<UIPanel>();
        dragPanel.depth = 1000;
    }

    private void OnEndDrag()
    {
        //Debug.Log("End drag");
        DropTile();
        Destroy(dragPanel);
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
        var zeroPos = Levels.ZeroPos;
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
        //} else if (position.x > Levels.ScrollBoundsMin.x && position.x < Levels.ScrollBoundsMax.x &&
        //           position.y > Levels.ScrollBoundsMin.y && position.y < Levels.ScrollBoundsMax.y)
        }
        else
        {
            //Position = Levels.ScrollPos;
            Position = pickPosition;
            //transform.localPosition = asfasf;
        }


        //Position = asfasf;

        transform.localPosition = Position;
    }

    private Vector3 pickPosition;
    private void PickTile()
    {
        pickPosition = this.transform.localPosition;


    }
}
