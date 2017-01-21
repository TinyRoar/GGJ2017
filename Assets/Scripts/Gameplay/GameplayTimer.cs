using System.Collections;
using System.Collections.Generic;
using TinyRoar.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTimer : MonoSingleton<GameplayTimer> {

    public Text Text;

    private float _time;

    void Start()
    {
    }

    public void Enable()
    {
        _time = Config.Instance.Timer;
        Updater.Instance.OnUpdate -= DoUpdate;
        Updater.Instance.OnUpdate += DoUpdate;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Updater.Instance.OnUpdate -= DoUpdate;

    }

    public void Disable()
    {
        Updater.Instance.OnUpdate -= DoUpdate;
    }

    void DoUpdate()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
            _time = 0;
        Text.text = (_time).ToString("n2").Replace('.', ':');

        if(_time == 0)
        {
            MatchManager.Instance.MatchWin = false;
            Events.GameplayStatus = GameplayStatus.MatchStop;
        }

    }

    public float GetTime()
    {
        return _time;
    }


}
