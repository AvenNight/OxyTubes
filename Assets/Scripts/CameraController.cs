using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Camera Camera;

    private void Start()
    {
        Camera = GetComponent<Camera>();
    }
}
