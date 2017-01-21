using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyRoar.Framework;
using XInputDotNetPure;

public class Player : MonoBehaviour {

    public float Speed = 4;
    public PlayerNumber PlayerNumber;
    public bool Invisible = false;
    public bool Vibration = false;
    private bool _curVisible = false;
    public Renderer Renderer;
    private float _visibilityValue = 0;
    private Vector3 _lastDistance;
    private float _antiCampTime;
    private bool _campVisible = false;

    void Start ()
    {
        if(PlayerNumber == PlayerNumber.None)
        {
            Debug.LogWarning("Player has no PlayerNumber");
            return;
        }

        PlayerManager.Instance.PlayerStorage.Add(this);
        Enable();
    }

    public void Enable()
    {
        Updater.Instance.OnUpdate -= DoUpdate;
        Updater.Instance.OnUpdate += DoUpdate;
    }

    public void Disable()
    {
        Updater.Instance.OnUpdate -= DoUpdate;
    }

    void OnDestroy()
    {
        Updater.Instance.OnUpdate -= DoUpdate;
        GamePad.SetVibration(GetPlayerIndex(), 0, 0);
    }

    void DoUpdate ()
    {
        DoMovement();
        DoAntiCamp();
        DoVisibility();

        DoWave();
        DoVibration();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoStomp();
        }
    }

    public void DoMovement()
    {
        // get input
        Vector3 input = Vector3.zero;
        input.x = Input.GetAxis(PlayerNumber.ToString() + "_Horizontal");
        input.z = Input.GetAxis(PlayerNumber.ToString() + "_Vertical");

        // check if doInvisible etc..
        if (Invisible)
        {
            // enable / disable visibility
            if(!_curVisible && input.magnitude > Config.Instance.VisibleSpeed)
            {
                _curVisible = true;
            }
            else if (_curVisible && input.magnitude < Config.Instance.VisibleSpeed)
            {
                _curVisible = false;
            }
        }

        // move
        transform.Translate(input * Speed * Time.deltaTime);

    }

    public void DoAntiCamp()
    {
        if (_lastDistance == null)
            _lastDistance = this.transform.position;

        _antiCampTime += Time.deltaTime;

        if(_antiCampTime >= 1)
        {
            if(Vector3.Distance(_lastDistance, this.transform.position) < Config.Instance.TooLowDistanceIn1Sec)
            {
                _campVisible = true;
            }
            else
            {
                _campVisible = false;
            }
            _antiCampTime = 0;
            _lastDistance = this.transform.position;
        }

    }

    public void DoVisibility()
    {
        // smooth increase / decrease of move visibility
        if (_curVisible)
        {
            _visibilityValue += Time.deltaTime / Config.Instance.MovingCompleteVisibleAfterSeconds;
            if (_visibilityValue >= 1)
                _visibilityValue = 1;
        }
        else if (!_campVisible)
        {
            _visibilityValue -= Time.deltaTime / Config.Instance.MovingCompleteInvisibleAfterSeconds;
            if (_visibilityValue < 0)
                _visibilityValue = 0;
        }

        // camp visibility
        if(_campVisible)
        {
            _visibilityValue += Time.deltaTime / Config.Instance.CampCompleteVisibleAfterSeconds;
            if (_visibilityValue >= 1)
                _visibilityValue = 1;
        }

        // TODO do this to a texture or whatever..
        if (Invisible)
        {
            if(_visibilityValue > 0)
            {
                Renderer.enabled = true;
                Debug.Log("Visible: " + _visibilityValue);
            }
            else
            {
                Renderer.enabled = false;
            }

        }

    }

    public void DoWave()
    {
        // spawn wave
        if (_curVisible)
        {
            // TODO trigger wave
        }

    }

    public void DoVibration()
    {
        if (!Vibration)
            return;

        // check distance between players 
        Player otherPlayer = PlayerManager.Instance.GetPlayerWithNotNumber(PlayerNumber);
        float distance = Vector3.Distance(this.transform.position, otherPlayer.transform.position);

        // do vibration
        if (distance < Config.Instance.VibrationDistance)
        {
            float vibrationValue = 1 - (distance / Config.Instance.VibrationDistance);

            if(Config.Instance.DoVibration)
                GamePad.SetVibration(GetPlayerIndex(), vibrationValue, vibrationValue);
        }

    }

    private void DoStomp()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, Config.Instance.StompRadius);

        foreach (Collider item in objectsInRange)
        {
            if (item.CompareTag("Player") && item.transform.parent != transform)
            {
                print("HAB DISCH!");
                Events.GameplayStatus = GameplayStatus.MatchStop;
            }
        }
    }

    private PlayerIndex GetPlayerIndex()
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Config.Instance.StompRadius);
    }

}
