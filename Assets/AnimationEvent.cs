using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

    public GameObject WaveParticlesPrefab;

    public Transform LeftFootTransform;
    public Transform RightFootTransform;

    private Vector3 spawnPos = Vector3.zero;

    public void LeftFoot ()
    {
        spawnPos = LeftFootTransform.position;
        spawnPos.y = 0;

        Instantiate(WaveParticlesPrefab, spawnPos, Quaternion.identity);

        this.transform.parent.GetComponent<Player>().TrySetValueToSkill<Movement>(2);
	}

    public void RightFoot()
    {
        spawnPos = RightFootTransform.position;
        spawnPos.y = 0;

        Instantiate(WaveParticlesPrefab, spawnPos, Quaternion.identity);

        this.transform.parent.GetComponent<Player>().TrySetValueToSkill<Movement>(3);

    }
}
