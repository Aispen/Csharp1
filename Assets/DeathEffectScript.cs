using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathEffectScript: MonoBehaviour {

    public ParticleSystem deathParticle;

    private void OnDestroy()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
    }
}
