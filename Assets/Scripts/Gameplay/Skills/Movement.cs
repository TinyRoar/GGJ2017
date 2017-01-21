using System;
using System.Collections;
using System.Collections.Generic;
using TinyRoar.Framework;
using UnityEngine;

public class Movement : Skill
{
    private bool _slowerMovementInDeepWater;
    private bool _isRunning;
    private Vector3 input = Vector3.zero;
    private int speedHash = Animator.StringToHash("MovementSpeed");
    private int _soundId = 1;
    private float _visibilityValue;

    public override void Enable()
    {
        _slowerMovementInDeepWater = false;
        _isRunning = false;
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
            // now deep water
            _slowerMovementInDeepWater = true;
        }
        else if(value == 10)
        {
            // deep water end
            _slowerMovementInDeepWater = false;
        }
        else if (value == 2)
        {
            // running begin
            _isRunning = true;
        }
        else if (value == 3)
        {
            // running end
            _isRunning = false;
        }
        else if (value == 4 || value == 5)
        {
            // play sound left/right foot

            String playerName = player.PlayerNumber == PlayerNumber.Player1 ? "Lono" : "Laka";

            String runOrStep = "Run";
            if(player.PlayerNumber == PlayerNumber.Player2)
            {
                if(_isRunning == false)
                    runOrStep = "Step";
                if (_visibilityValue <= 0.5)
                    return;
            }
            else if (player.PlayerNumber == PlayerNumber.Player1 && _slowerMovementInDeepWater == true)
                runOrStep = "Deep";

            //Debug.Log("play sound " + playerName + runOrStep + _soundId);
            SoundManager.Instance.Play(playerName + runOrStep + _soundId, SoundManager.SoundType.Soundeffect, false, 1);

            _soundId++;
            if (_soundId == 3)
                _soundId = 1;

        }
        else
        {
            _visibilityValue = value;
        }
    }

}
