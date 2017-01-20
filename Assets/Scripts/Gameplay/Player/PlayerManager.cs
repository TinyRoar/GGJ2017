using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyRoar.Framework;

public class PlayerManager : MonoSingleton<PlayerManager> {

    public List<Player> PlayerStorage = new List<Player>();
    public List<GameObject> PlayerPrefabs = new List<GameObject>();
    public List<Transform> SpawnPoints = new List<Transform>();

    void Start()
    {
    }

    public void SpawnPlayer()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            GameObject.Instantiate(PlayerPrefabs[i], SpawnPoints[i].position, Quaternion.identity);
        }
    }

    public Player GetPlayerWithNumber(PlayerNumber playerNo)
    {
        foreach (var player in PlayerStorage)
        {
            if (player.PlayerNumber == playerNo)
                return player;
        }
        return null;
    }

    public Player GetPlayerWithNotNumber(PlayerNumber playerNo)
    {
        foreach (var player in PlayerStorage)
        {
            if (player.PlayerNumber != playerNo)
                return player;
        }
        return null;
    }

}
