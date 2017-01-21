using System;
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
            case GameplayStatus.Menu:
                break;
            case GameplayStatus.MatchStart:

                // Start Timer
                GameplayTimer.Instance.Enable();

                // TODO Spawn Players
                PlayerManager.Instance.SpawnPlayer();

                break;

            case GameplayStatus.MatchStop:

                // Stop Timer
                GameplayTimer.Instance.Disable();

                // UI
                UIManager.Instance.Switch(Layer.InGame, UIAction.Hide);
                UIManager.Instance.Switch(Layer.MainMenu, UIAction.Show);
                UIManager.Instance.Switch(Layer.End, UIAction.Show);
                UIHandling.Instance.DoEndUI();

                // TODO Disable or Destroy Players
                PlayerManager.Instance.ResetPlayer();

                break;
        }

    }


}
