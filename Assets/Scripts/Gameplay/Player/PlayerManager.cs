using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyRoar.Framework;

public class PlayerManager : MonoSingleton<PlayerManager> {

    public List<Player> PlayerStorage = new List<Player>();
    public List<GameObject> PlayerPrefabs = new List<GameObject>();
    public List<Transform> SpawnPoints = new List<Transform>();

    private bool _spawned = false;

    void Start()
    {
    }

    public void SpawnPlayer()
    {
        if (_spawned)
            return;
        _spawned = true;

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            GameObject.Instantiate(PlayerPrefabs[i], SpawnPoints[i].position, Quaternion.identity);
        }
    }

    public void ResetPlayer()
    {
        for (int i = 0; i < PlayerStorage.Count; i++)
        {
            PlayerStorage[i].transform.position = SpawnPoints[i].position;
            PlayerStorage[i].transform.rotation = Quaternion.identity;
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
