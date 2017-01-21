using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyRoar.Framework;

public class PlayerManager : MonoSingleton<PlayerManager> {

    public List<Player> PlayerStorage = new List<Player>();
    public List<GameObject> PlayerPrefabs = new List<GameObject>();
    public List<Transform> SpawnPoints = new List<Transform>();

    public bool Player1and2Changed = true;

    private bool _spawned = false;

    void Start()
    {
    }

    public void SpawnPlayer()
    {
        if (_spawned)
        {
            for (int i = 0; i < PlayerStorage.Count; i++)
                PlayerStorage[i].Enable();

            return;
        }
        _spawned = true;

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            GameObject temp = GameObject.Instantiate(PlayerPrefabs[i], SpawnPoints[i].position, Quaternion.identity);
            temp.transform.parent = UIManager.Instance.GetEnvironment(GameEnvironment.Default);
        }
    }

    public void ResetPlayer()
    {
        for (int i = 0; i < PlayerStorage.Count; i++)
        {
            PlayerStorage[i].transform.position = SpawnPoints[i].position;
            PlayerStorage[i].transform.rotation = Quaternion.identity;
            PlayerStorage[i].Disable();
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
