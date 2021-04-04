using UnityEngine;

public class SceneController : MonoBehaviour
{
    public SceneCreator SceneCreator;
    public UITileScroll UITileScroll;
    public Timer Timer;

    private void OnEnable()
    {
        if (ArtCollection.Instance != null)
            Start();
    }

    private void Start()
    {
        SceneCreator.CreateScene(Levels.Level1);
        UITileScroll.Set(Levels.GetTilesData(Levels.Level1Tubes));
        Timer.Set(120f, 0.1f);
    }
}
