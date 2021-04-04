using System;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    public SceneCreator SceneCreator;
    public UITileScroll UITileScroll;
    public Timer Timer;

    public TubeData[,] LevelTubeData;

    public GameObject WinLabel;
    public UI2DSprite LevelBack;
    public Sprite Level2;

    private int curLvl;

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
        StartLevel1();
    }

    public void StartLevel1()
    {
        curLvl = 1;
        LevelTubeData = SceneCreator.CreateScene(Levels.Level1);
        UITileScroll.Set(Levels.GetTilesData(Levels.Level1Tubes));
        Timer.Set(120f, 0.1f);
    }

    public void StartLevel2()
    {
        LevelBack.sprite2D = Level2;
        curLvl = 2;
        LevelTubeData = SceneCreator.CreateScene(Levels.Level2);
        UITileScroll.Set(Levels.GetTilesData(Levels.Level2Tubes));
        Timer.Set(180f, 0.1f);
    }

    private bool[,] levelChecked;
    public bool LevelCheck()
    {
        levelChecked = new bool[LevelTubeData.GetLength(0), LevelTubeData.GetLength(1)];

        bool result = false;

        switch (curLvl)
        {
            case 1:
                result = LevelCheck(LevelTubeData[4, 0], 4, 0);
                break;
            case 2:
                result = LevelCheck(LevelTubeData[6, 0], 6, 0);
                break;
        }
        //var result = 
        //    LevelCheck(LevelTubeData[4, 0], 4, 0);

        for (int x = LevelTubeData.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = 0; y < LevelTubeData.GetLength(1); y++)
            {
                if (LevelTubeData[x, y] != null && LevelTubeData[x,y].TileType == TileType.City)
                {
                    if (!levelChecked[x, y])
                        result = false;
                }
            }
        }

        if (result)
            Win();

        return result;
    }

    public void Win()
    {
        WinLabel.SetActive(true);
    }

    public void WinClick()
    {
        //WinLabel.SetActive(false);
        WinLabel.GetComponent<TweenAlpha>().PlayReverse();
        StartLevel2();
    }

    public bool LevelCheck(TubeData td, int x, int y)
    {
        int maxX = LevelTubeData.GetLength(0) - 1;
        int maxY = LevelTubeData.GetLength(1) - 1;

        if (td == null)
            return false;

        levelChecked[x, y] = true;

        Func<bool> nextCheck = null;

        if (td.Up && x - 1 >= 0 && x - 1 <= maxX)
        {
            var nx = x - 1;
            var next = LevelTubeData[nx, y];

            if (next == null)
                return false;

            if (next != null && next.Down && !levelChecked[nx, y])
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
