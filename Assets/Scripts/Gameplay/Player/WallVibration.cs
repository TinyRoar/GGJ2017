using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class WallVibration : MonoBehaviour {

    public Player _player;

    private void OnTriggerEnter(Collider other)
    {
        // do vibration oneshot
        if (_player == null)
            _player = this.transform.parent.GetComponent<Player>();

        float vibrationValue = 0.1f;
        GamePad.SetVibration(_player.GetPlayerIndex(), vibrationValue, vibrationValue);

    }

}
