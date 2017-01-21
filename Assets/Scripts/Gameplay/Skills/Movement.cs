using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Skill
{

    public override void Enable()
    {
    }

    public override void Disable()
    {
    }

    public override void Destroy()
    {
    }

    public override void DoUpdate()
    {
        Action();
    }

    private void Action()
    {
        // get input
        Vector3 input = Vector3.zero;
        input.x = Input.GetAxis(player.PlayerNumber.ToString() + "_Horizontal");
        input.z = Input.GetAxis(player.PlayerNumber.ToString() + "_Vertical");

        // move
        player.transform.Translate(input * Config.Instance.VisibleSpeed * Time.deltaTime);

    }
}
