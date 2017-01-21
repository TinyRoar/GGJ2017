using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotParticle : MonoBehaviour {

    private ParticleSystem particle;

	void Start ()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update ()
    {
        if (particle.isStopped)
            Destroy(transform.parent.gameObject);
	}
}
