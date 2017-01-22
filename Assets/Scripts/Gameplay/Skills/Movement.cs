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
    private float speed = 0;
    private float motion = 0;
    private bool _isKeyboardSneak = false;

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

        if (Input.GetKeyDown(KeyCode.RightShift) && _isKeyboardSneak == false)
        {
            _isKeyboardSneak = true;
            player.TrySetValueToSkill<Invisibility>(20);

        }
        else if (Input.GetKeyUp(KeyCode.RightShift) && _isKeyboardSneak == true)
        {
            _isKeyboardSneak = false;
            player.TrySetValueToSkill<Invisibility>(30);

        }

    }

    private void GetInput()
    {
        // get input
        input = Vector3.zero;
        input.x = base.player.GetGamePadState().ThumbSticks.Left.X + Input.GetAxis(player.PlayerNumber.ToString() + "_Horizontal");
        input.z = base.player.GetGamePadState().ThumbSticks.Left.Y + Input.GetAxis(player.PlayerNumber.ToString() + "_Vertical");

        //input.x = Input.GetAxis(player.PlayerNumber.ToString() + "_Horizontal");
        //input.z = Input.GetAxis(player.PlayerNumber.ToString() + "_Vertical");
    }

    private void Action()
    {
        // check deep water
        float multiply = 1;
        if (_slowerMovementInDeepWater)
            multiply = Config.Instance.DeepWaterMultiply;

        // move
        speed = Config.Instance.Speed;
        if(player.PlayerNumber == PlayerNumber.Player2)
            speed = Config.Instance.SpeedPlayer2;
        if (_isKeyboardSneak)
            speed *= 0.8f;
        player.transform.Translate(input * speed * Time.deltaTime * multiply);

        if (_slowerMovementInDeepWater)
            motion = 1;
        else
            motion = input.magnitude * speed * multiply;
    }

    private void Animation()
    {
        // rotate char model to input direction
        if (base.player.ModelTransform == null)
            return;

        base.player.ModelAnimator.SetFloat(speedHash, motion);

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
            SoundManager.Instance.Play(playerName + runOrStep + _soundId, SoundManager.SoundType.Soundeffect, false, Config.Instance.StepsVolume);

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
