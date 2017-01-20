using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Speed = 4;
    public PlayerNo PlayerNumber;
    public bool Invisible = false;
    private bool _curVisible = false;
    public Renderer Renderer;

	void Start ()
    {

	}
	
	void Update ()
    {
        Movement();
    }

    void Movement()
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

        // spawn wave
        if (_curVisible)
        {
            Debug.Log("trigger wave");
        }

    }
}
