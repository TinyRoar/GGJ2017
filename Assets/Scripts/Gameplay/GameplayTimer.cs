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
        _time = 0;
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
        _time += Time.deltaTime;
        Text.text = _time.ToString();
    }

    public float GetTime()
    {
        return _time;
    }


}
