using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public SceneCreator sceneCreator;

    private void OnEnable()
    {
        //sceneCreator.CreateScene(Levels.Level1);
        sceneCreator.CreateScene(Levels.Level2);
    }
}
