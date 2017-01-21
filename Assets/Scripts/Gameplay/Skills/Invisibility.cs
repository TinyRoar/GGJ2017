using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Invisibility : Skill
{
    private bool _curVisible = false;
    private float _visibilityValue = 0;
    private Vector3 _lastDistance;
    private float _antiCampTime;
    private bool _campVisible = false;
    private Material _material;

    public override void SetPlayer(Player player)
    {
        base.SetPlayer(player);

        Material[] materials = player.transform.Find("Laka/Laka").GetComponent<SkinnedMeshRenderer>().materials;
        foreach (var material in materials)
        {
            if (material.name == "MatLaka (Instance)")
            {
                _material = material;
            }
        }
    }

    public override void Enable()
    {
        _lastDistance = player.transform.position;
    }

    public override void Disable()
    {
    }

    public override void Destroy()
    {
    }

    public override void DoUpdate()
    {
        DoAntiCamp();
        DoVisibility();
    }

    public void DoAntiCamp()
    {
        _antiCampTime += Time.deltaTime;

        if (_antiCampTime >= Config.Instance.CampCheckInterval)
        {
            if (Vector3.Distance(_lastDistance, player.transform.position) < Config.Instance.CampDistance)
            {
                _campVisible = true;
            }
            else
            {
                _campVisible = false;
            }
            _antiCampTime = 0;
            _lastDistance = player.transform.position;
        }

    }

    public void DoVisibility()
    {
        Vector3 input = Vector3.zero;
        input.x = Input.GetAxis(player.PlayerNumber.ToString() + "_Horizontal");
        input.z = Input.GetAxis(player.PlayerNumber.ToString() + "_Vertical");

        // enable / disable visibility
        if (!_curVisible && input.magnitude > Config.Instance.InvisibleSpeed)
        {
            _curVisible = true;
            player.TrySetValueToSkill<Movement>(2);
        }
        else if (_curVisible && input.magnitude < Config.Instance.InvisibleSpeed)
        {
            _curVisible = false;
            player.TrySetValueToSkill<Movement>(3);
        }

        // smooth increase / decrease of move visibility
        if (_curVisible)
        {
            _visibilityValue += Time.deltaTime / Config.Instance.MovingVisibleSec;
            if (_visibilityValue >= 1)
                _visibilityValue = 1;
        }
        else if (!_campVisible)
        {
            _visibilityValue -= Time.deltaTime / Config.Instance.MovingVanishSec;
            if (_visibilityValue < 0)
                _visibilityValue = 0;
        }

        // camp visibility
        if (_campVisible)
        {
            _visibilityValue += Time.deltaTime / Config.Instance.CampVisibleSec;
            if (_visibilityValue >= 1)
                _visibilityValue = 1;
        }

        if(_visibilityValue < 1)
            player.TrySetValueToSkill<Movement>(_visibilityValue);

        // update visibility
        Color color = _material.color;
        color.a = _visibilityValue;
        _material.color = color;

    }

}