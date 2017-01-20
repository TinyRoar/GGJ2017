﻿using System;
using UnityEngine;
using TinyRoar.Framework;

public class MatchManager : Singleton<MatchManager>
{
    public void Init()
    {
        Events.Instance.OnGameplayStatusChange += GameplayStatusChange;
    }

    void GameplayStatusChange(GameplayStatus oldMatchStatus, GameplayStatus newMatchStatus)
    {

        switch (newMatchStatus)
        {
            case GameplayStatus.MatchStart:
                UIManager.Instance.Switch(Layer.InGame, UIAction.Show);
                UIManager.Instance.Switch(Layer.MainMenu, UIAction.Show);
                break;
        }

    }


}
