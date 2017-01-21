using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TinyRoar.Framework;
using UnityEngine.SceneManagement;

public class Config : MonoSingleton<Config>
{

    // Player

    [Header("Movement")]
    public float Speed = 5f; // in meters per Second
    public float MovingVisibleSec = 1f;
    public float MovingVanishSec = 1f;
    public float CampVisibleSec = 2f;
    public float DeepWaterMultiply = 0.4f;

    [Header("Invisibility")]
    public float InvisibleSpeed = 0.8f; // ab dieser geschwindigkeit (0 bis 1) wird man sichtbarer
    public float CampDistance = 2f;
    public float CampCheckInterval = 2f;

    [Header("Vibration")]
    public bool DoVibration = false;
    public float VibrationDistance = 10f; // in meters pro sekunde -> für vibration

    [Header("Stomp")]
    public float StompRadius = 5f;
    public float StompCoolDown = 5f;

    [Header("Signal")]
    public float SignalRadius = 5f;
    public float SignalCoolDown = 5f;

    [Header("Sound")]
    public float StepsVolume = 0.7f;


    // Framework

    [Header("Framework")]
    public Vector2i BaseScreenSize;

}