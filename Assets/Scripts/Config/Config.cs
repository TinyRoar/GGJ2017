using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TinyRoar.Framework;
using UnityEngine.SceneManagement;

public class Config : MonoSingleton<Config>
{
    [Header("Map Animation")]
    public float SpawnHeight = 21f;
    public float MaxDelayTime = 0.9f;
    public float FallDownSpeed = 1.4f;
    public float UnitPosY = 0.03f;
    public Vector2i MapSize;
    public int BaseUnitsLeft = 4;


    public List<string> SceneList;

    public static string MapPath = "Export/";
    public static string PrefabPath = "Prefabs/";

    // Config
    public Vector2i BaseScreenSize;


}