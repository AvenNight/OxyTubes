using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void HideMenu()
    {
        SceneController.Instance.StartLevel1();
        gameObject.SetActive(false);
    }
}
