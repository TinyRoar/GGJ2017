using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowerInDeepWater : Skill
{
    private bool _activated = false;

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
    }

    public override void SetValue(float value)
    {
        if (value == 1 && _activated == false)
        {
            _activated = true;
            player.TrySetValueToSkill<Movement>(1);
        }
        if (value == 10 && _activated == true)
        {
            _activated = false;
            player.TrySetValueToSkill<Movement>(10);
        }
    }
}
