using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    public UI2DSprite Empty1;



    private int step => Empty1.height;
    private Vector2 step2 => Empty1.localSize;

    public Transform MapRoot;

    public Tile Empty;
    public Tile Wall;

    public void CreateScene(char [,] map)
    {
        Debug.Log($"{step}");

        var curPos = new Vector2(0, 0);


        GameObject cur;
        Tile tile = new Tile();

        //var xx = map.GetLength(0);
        //var yy = map.GetLength(1);

        //for (int x = 0; x < map.GetLength(0); x++)
        for (int x = map.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            //for (int y = map.GetLength(1) - 1; y >= 0; y--)
            {
                //Tile cur;
                //GameObject cur;
                //switch (map[x, y])
                switch (map[x, y])
                {
                    case ' ':
                    default:
                        cur = Instantiate(Empty.gameObject, MapRoot);
                        
                        break;
                    case 'w':
                        cur = Instantiate(Wall.gameObject, MapRoot);
                        break;
                }

                tile = cur.GetComponent<Tile>();
                //var obj = Instantiate(Empty1, MapRoot);
                cur.transform.localPosition = curPos;
                curPos.x += tile.Sprite.width;


                Debug.Log($"{x} {y}");
            }
            curPos.x = 0;
            curPos.y += tile.Sprite.height;
        }
    }
}
