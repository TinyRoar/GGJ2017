using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Skill
{
    private bool _slowerMovementInDeepWater;

    public override void Enable()
    {
        _slowerMovementInDeepWater = false;
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

        // check deep water
        float multiply = 1;
        if (_slowerMovementInDeepWater)
            multiply = Config.Instance.DeepWaterMultiply;

        // move
        player.transform.Translate(input * Config.Instance.Speed * Time.deltaTime * multiply);

    }

    public override void SetValue(float value)
    {
        if(value == 1)
        {
            _slowerMovementInDeepWater = true;
        }
        else if(value == 0)
        {
            _slowerMovementInDeepWater = false;
        }
    }

}
