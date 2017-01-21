using System;
using System.Collections;
using System.Collections.Generic;
using TinyRoar.Framework;
using UnityEngine;

public class Stomp : Skill
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

        if((player.pressedA || Input.GetKeyDown(KeyCode.Space)) && _cooldownTime >= Config.Instance.StompCoolDown)
        {
            _cooldownTime = 0;
            DoStomp();
        }

    }

    private void DoStomp()
    {

        Collider[] objectsInRange = Physics.OverlapSphere(player.transform.position, Config.Instance.StompRadius);

        foreach (Collider item in objectsInRange)
        {
            if (item.CompareTag("Player") && item.transform.parent != player.transform)
            {
                // HAB DISCH
                Events.GameplayStatus = GameplayStatus.MatchStop;

                SoundManager.Instance.Play("LonoHey", SoundManager.SoundType.Soundeffect, false, 0.5f);

            }
        }

    }

}

