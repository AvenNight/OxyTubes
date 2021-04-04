using System;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    public SceneCreator SceneCreator;
    public UITileScroll UITileScroll;
    public Timer Timer;

    public TubeData[,] LevelTubeData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (ArtCollection.Instance != null)
            Start();
    }

    private void Start()
    {
        LevelTubeData = SceneCreator.CreateScene(Levels.Level1);
        UITileScroll.Set(Levels.GetTilesData(Levels.Level1Tubes));
        Timer.Set(120f, 0.1f);
    }


    private bool[,] levelChecked;
    //private bool[,] levelChecked;
    public bool LevelCheck()
    {
        levelChecked = new bool[LevelTubeData.GetLength(0), LevelTubeData.GetLength(1)];

        return LevelCheck(LevelTubeData[4, 0], 4, 0);

        //return false;
    }

    public int TEST = 0;
    public bool LevelCheck(TubeData td, int x, int y)
    {
        int maxX = LevelTubeData.GetLength(0) - 1;
        int maxY = LevelTubeData.GetLength(1) - 1;

        if (td == null)// || levelChecked[x, y])
            return false;

        levelChecked[x, y] = true;

        //Debug.Log(TEST);
        TEST++;

        Func<bool> nextCheck = null;

        if (td.Up && x - 1 >= 0 && x - 1 <= maxX)
        {
            var nx = x - 1;
            var next = LevelTubeData[nx, y];

            if (next == null)
                return false;

            if (next != null && next.Down && !levelChecked[nx, y])
                //return LevelCheck(LevelTubeData[nx, y], nx, y);
                nextCheck += () => LevelCheck(LevelTubeData[nx, y], nx, y);
        }
            
        
        if (td.Down && x + 1 >= 0 && y + 1 <= maxX)
        {
            var nx = x + 1;
            var next = LevelTubeData[nx, y];

            if (next == null)
                return false;

            if (next != null && next.Up && !levelChecked[nx, y])
                nextCheck += () => LevelCheck(LevelTubeData[nx, y], nx, y);
        }


        if (td.Left && y - 1 >= 0 && y - 1 <= maxY)
        {
            var ny = y - 1;
            var next = LevelTubeData[x, ny];

            if (next == null)
                return false;

            if (next != null && next.Right && !levelChecked[x, ny])
                nextCheck += () => LevelCheck(LevelTubeData[x, ny], x, ny);
        }

        if (td.Right && y + 1 >= 0 && y + 1 <= maxY)
        {
            var ny = y + 1;
            var next = LevelTubeData[x, ny];

            if (next == null)
                return false;

            if (next != null && next.Left && !levelChecked[x, ny])
                nextCheck += () => LevelCheck(LevelTubeData[x, ny], x, ny);
        }

        if (nextCheck == null)
            return true;
        else
            return nextCheck.Invoke();
    }
}
