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

        if (player.pressedB && _cooldownTime >= Config.Instance.SignalCoolDown)
        {
            _cooldownTime = 0;

            // play sound
            //SoundManager.Instance.Play("signal1", SoundManager.SoundType.Soundeffect, false, 1);

            // play sound 2
            Player otherPlayer = player.GetOtherPlayer();
            float volume = Vector3.Distance(player.transform.position, otherPlayer.transform.position) / Config.Instance.SignalRadius;
            SoundManager.Instance.Play("LakaLaughter" + UnityEngine.Random.Range(1, 2), SoundManager.SoundType.Soundeffect, false, volume, 2f);

        }

    }

}

