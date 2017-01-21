using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Skill
{
    private bool _slowerMovementInDeepWater;
    private Vector3 input = Vector3.zero;
    private int speedHash = Animator.StringToHash("MovementSpeed");

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
        GetInput();
        Action();
        Animation();
    }

    private void GetInput()
    {
        // get input
        input = Vector3.zero;
        input.x = Input.GetAxis(player.PlayerNumber.ToString() + "_Horizontal");
        input.z = Input.GetAxis(player.PlayerNumber.ToString() + "_Vertical");
    }

    private void Action()
    {
        // check deep water
        float multiply = 1;
        if (_slowerMovementInDeepWater)
            multiply = Config.Instance.DeepWaterMultiply;

        // move
        player.transform.Translate(input * Config.Instance.Speed * Time.deltaTime * multiply);

    }

    private void Animation()
    {
        // rotate char model to input direction
        if (base.player.ModelTransform == null)
            return;

        base.player.ModelAnimator.SetFloat(speedHash, input.magnitude);

        if (input != Vector3.zero)
            base.player.ModelTransform.localRotation = Quaternion.LookRotation(input, Vector3.up);
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
