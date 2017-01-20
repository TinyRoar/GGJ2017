using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TinyRoar.Framework;
using UnityEngine.SceneManagement;

public class Config : MonoSingleton<Config>
{
    // Config
    public Vector2i BaseScreenSize;

    // Player
    [Header("Player")]
    public float VisibleSpeed = 0.8f;
    public float VibrationDistance = 10f;
    public bool DoVibration = false;

}