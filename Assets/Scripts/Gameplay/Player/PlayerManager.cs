using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyRoar.Framework;

public class PlayerManager : Singleton<PlayerManager> {

    public List<Player> PlayerStorage = new List<Player>();

    void Start()
    {
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
