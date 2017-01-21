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
    public float Speed = 0.8f;
    public float VibrationDistance = 10f;
    public bool DoVibration = false;
    public float StompRadius = 5f;
    public float StompCoolDown = 5f;
    public float MovingVisibleSec = 1f;
    public float MovingVanishSec = 1f;
    public float CampVisibleSec = 2f;
    public float CampVanishSec = 2f;
    public float TooLowDistanceIn1Sec = 2f;

}