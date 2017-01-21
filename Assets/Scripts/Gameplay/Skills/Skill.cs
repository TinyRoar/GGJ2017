using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {

    protected Player player;

    public virtual void SetPlayer(Player player)
    {
        this.player = player;
    }

    public abstract void Enable();
    public abstract void Disable();
    public abstract void Destroy();
    public abstract void DoUpdate();
    public virtual void SetValue(float value)
    {

    }

}
