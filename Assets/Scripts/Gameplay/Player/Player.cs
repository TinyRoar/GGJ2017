using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Speed = 4;
    public PlayerNo PlayerNumber;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Input.GetAxis(PlayerNumber.ToString()+"_Horizontal") * Speed * Time.deltaTime, 0, Input.GetAxis(PlayerNumber.ToString() + "_Vertical") * Speed * Time.deltaTime);
    }
}
