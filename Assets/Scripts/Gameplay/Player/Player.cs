using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyRoar.Framework;
using XInputDotNetPure;

public class Player : MonoBehaviour {

    public PlayerNumber PlayerNumber;
    public Renderer Renderer;

    public Transform ModelTransform;
    public Animator ModelAnimator;

    public List<SkillType> SkillTypeList;
    public List<Skill> SkillList;

    private GamePadState state;
    private GamePadState prevState;
    public bool pressedA { get; private set; }
    public bool pressedB { get; private set; }

    void Start ()
    {
        // init vars
        SkillList = new List<Skill>();

        if (PlayerNumber == PlayerNumber.None)
        {
            Debug.LogWarning("Player has no PlayerNumber");
            return;
        }

        PlayerManager.Instance.PlayerStorage.Add(this);

        // add Skills
        foreach (var skillType in SkillTypeList)
        {
            Skill skill = CodeHelper.CreateInstance<Skill>(skillType.ToString());
            skill.SetPlayer(this);
            SkillList.Add(skill);
        }

        // enable all skills
        Enable();
    }

    public void Enable()
    {
        pressedA = false;
        pressedB = false;

        Updater.Instance.OnUpdate -= DoUpdate;
        Updater.Instance.OnUpdate += DoUpdate;

        foreach (var skill in SkillList)
        {
            skill.Enable();
        }
    }

    public void Disable()
    {
        Updater.Instance.OnUpdate -= DoUpdate;

        foreach (var skill in SkillList)
        {
            skill.Disable();
        }
    }

    void OnDestroy()
    {
        Updater.Instance.OnUpdate -= DoUpdate;

        foreach (var skill in SkillList)
        {
            skill.Destroy();
        }
    }

    void DoUpdate ()
    {
        foreach (var skill in SkillList)
        {
            skill.DoUpdate();
        }
    }

    public PlayerIndex GetPlayerIndex()
    {
        switch(PlayerNumber)
        {
            case PlayerNumber.Player1:
                return PlayerIndex.One;
            case PlayerNumber.Player2:
                return PlayerIndex.Two;
        }
        return PlayerIndex.One;
    }

    public Player GetOtherPlayer()
    {
        return PlayerManager.Instance.GetPlayerWithNotNumber(this.PlayerNumber);
    }

    void OnDrawGizmos()
    {
        if (Config.Instance == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Config.Instance.StompRadius);
    }

    public void TrySetValueToSkill<T>(float value)
    {
        foreach (var skill in SkillList)
        {
            if (skill is T)
                skill.SetValue(value);
        }
    }

    private void DoControls()
    {
        prevState = state;
        state = GamePad.GetState(GetPlayerIndex());

        // Detect if a button was pressed/release this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            pressedA = true;
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
            pressedA = false;

        // Detect if a button was pressed/release this frame
        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
            pressedB = true;
        if (prevState.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released)
            pressedB = false;
    }

}
