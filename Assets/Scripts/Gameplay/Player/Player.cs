using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Speed = 4;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * Speed * Time.deltaTime);
    }
}
