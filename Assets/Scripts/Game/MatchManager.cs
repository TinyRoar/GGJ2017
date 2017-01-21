using System;
using UnityEngine;
using TinyRoar.Framework;

public class MatchManager : Singleton<MatchManager>
{
    private AudioSource _ingameAudio;

    public bool MatchWin = false;

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

                // change players every round
                PlayerManager.Instance.Player1and2Changed = PlayerManager.Instance.Player1and2Changed ? false : true;

                if (_ingameAudio != null)
                    SoundManager.Instance.Stop(_ingameAudio);
                _ingameAudio = SoundManager.Instance.Play("WaterAmbience1", SoundManager.SoundType.Music, true, 1);

                // Start Timer
                GameplayTimer.Instance.Enable();

                // TODO Spawn Players
                PlayerManager.Instance.SpawnPlayer();

                break;

            case GameplayStatus.MatchStop:

                // Stop Timer
                GameplayTimer.Instance.Disable();

                if(MatchWin)
                {
                    // TODO show/hide
                }

                // UI
                UIManager.Instance.Switch(Layer.InGame, UIAction.Hide);
                UIManager.Instance.Switch(Layer.MainMenu, UIAction.Show);
                UIManager.Instance.Switch(Layer.End, UIAction.Show);
                UIHandling.Instance.DoEndUI();

                UIManager.Instance.Switch(GameEnvironment.Menu);

                // TODO Disable or Destroy Players
                PlayerManager.Instance.ResetPlayer();



                break;
        }
    }


}
