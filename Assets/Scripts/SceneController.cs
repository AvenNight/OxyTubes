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
}
