using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Speed = 4;
    public enum playerNr { Player1_, Player2_};
    public playerNr PlayerNumber;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Input.GetAxis(PlayerNumber.ToString()+"Horizontal") * Speed * Time.deltaTime, 0, Input.GetAxis(PlayerNumber.ToString() + "Vertical") * Speed * Time.deltaTime);
    }
}
