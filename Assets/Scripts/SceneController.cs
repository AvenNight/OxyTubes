using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public SceneCreator sceneCreator;
    public Vector2 ZeroPos;
    public Vector2 TileSize;
    public char[,] CurLevel = Levels.Level3;

    private void OnEnable()
    {
        sceneCreator.CreateScene(CurLevel);
    }
}
