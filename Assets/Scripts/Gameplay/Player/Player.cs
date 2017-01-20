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

    void Start ()
    {
        if(PlayerNumber == PlayerNumber.None)
        {
            Debug.LogWarning("Player has no PlayerNumber");
            return;
        }

        PlayerManager.Instance.PlayerStorage.Add(this);
        Updater.Instance.OnUpdate += DoUpdate;
    }

    void OnDestroy()
    {
        Updater.Instance.OnUpdate -= DoUpdate;
        GamePad.SetVibration(GetPlayerIndex(), 0, 0);
    }

    void DoUpdate ()
    {
        DoMovement();
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
            if(!_curVisible && input.magnitude > Config.Instance.VisibleSpeed)
            {
                Renderer.enabled = true;
                _curVisible = true;
            }
            else if (_curVisible && input.magnitude < Config.Instance.VisibleSpeed)
            {
                Renderer.enabled = false;
                _curVisible = false;
            }
        }

        // move
        transform.Translate(input * Speed * Time.deltaTime);
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
