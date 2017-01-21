using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Vibration : Skill
{

    public override void Enable()
    {
    }

    public override void Disable()
    {
        GamePad.SetVibration(player.GetPlayerIndex(), 0, 0);
    }

    public override void Destroy()
    {
        GamePad.SetVibration(player.GetPlayerIndex(), 0, 0);
    }

    public override void DoUpdate()
    {
        // dont vibrate if no controller connected
        if (GamePad.GetState(base.player.GetPlayerIndex()).IsConnected == false)
            return;
        
        // check distance between players 
        Player otherPlayer = player.GetOtherPlayer();
        float distance = Vector3.Distance(player.transform.position, otherPlayer.transform.position);

        // do vibration
        if (distance < Config.Instance.VibrationDistance)
        {
            float vibrationValue = 1 - (distance / Config.Instance.VibrationDistance);

            if (Config.Instance.DoVibration)
                GamePad.SetVibration(player.GetPlayerIndex(), vibrationValue, vibrationValue);
        }
    }

}

