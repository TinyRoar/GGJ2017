using System;
using System.Collections;
using System.Collections.Generic;
using TinyRoar.Framework;
using UnityEngine;

public class Signal : Skill
{
    private float _cooldownTime;

    public override void Enable()
    {
        _cooldownTime = 0;
    }

    public override void Disable()
    {
    }

    public override void Destroy()
    {
    }

    public override void DoUpdate()
    {
        _cooldownTime += Time.deltaTime;

        if ((player.pressedA || Input.GetKeyDown(KeyCode.Space)) && _cooldownTime >= Config.Instance.SignalCoolDown)
        {
            _cooldownTime = 0;

            GameObject.Instantiate(base.player.WhistleParticlePrefab, base.player.transform.position, Quaternion.identity);

            // play sound 1
            Timer.Instance.Add(0.01f, delegate
            {
                if (Events.GameplayStatus == GameplayStatus.MatchStop)
                    return;
                SoundManager.Instance.Play("LonoWhistle", SoundManager.SoundType.Soundeffect, false, 1);
            });

            // play sound 2
            Player otherPlayer = player.GetOtherPlayer();
            float distance = Vector3.Distance(player.transform.position, otherPlayer.transform.position);
            if (distance > Config.Instance.SignalRadius)
                return;

            float volume = 1 - distance / Config.Instance.SignalRadius;
            float delay = 1;
            Timer.Instance.Add(delay, delegate
            {
                if (Events.GameplayStatus == GameplayStatus.MatchStop)
                    return;
                SoundManager.Instance.Play("LakaLaughter" + UnityEngine.Random.Range(1, 2), SoundManager.SoundType.Soundeffect, false, volume);
            });

        }

    }

}

