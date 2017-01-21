using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedAnimation : MonoBehaviour {

    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    void Update () {
        this.transform.position += Position * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(
            this.transform.rotation.x + Rotation.x * Time.deltaTime,
            this.transform.rotation.y + Rotation.y * Time.deltaTime,
            this.transform.rotation.z + Rotation.z * Time.deltaTime
        );
        this.transform.localScale += Scale * Time.deltaTime;
    }
}
