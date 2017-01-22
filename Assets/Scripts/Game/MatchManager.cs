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

                // disable winner gameobjects
                UIManager.Instance.GetEnvironment(GameEnvironment.End).Find("LonoWinner").gameObject.SetActive(false);
                UIManager.Instance.GetEnvironment(GameEnvironment.End).Find("LakaWinner").gameObject.SetActive(false);

                if (MatchWin)
                {
                    UIManager.Instance.Switch(GameEnvironment.End);
                    UIManager.Instance.GetEnvironment(GameEnvironment.End).Find("LonoWinner").gameObject.SetActive(true);
                    UIManager.Instance.Switch(Layer.LonoWin, UIAction.Show);
                    
                }
                else
                {
                    UIManager.Instance.Switch(GameEnvironment.End);
                    UIManager.Instance.GetEnvironment(GameEnvironment.End).Find("LakaWinner").gameObject.SetActive(true);
                    UIManager.Instance.Switch(Layer.LakaWin, UIAction.Show);
                }

                // UI
                UIManager.Instance.Switch(Layer.InGame, UIAction.Hide);
                UIManager.Instance.Switch(Layer.MainMenu, UIAction.Show);
                UIHandling.Instance.DoEndUI();

                UIManager.Instance.Switch(GameEnvironment.End);

                // TODO Disable or Destroy Players
                PlayerManager.Instance.ResetPlayer();



                break;
        }
    }


}
