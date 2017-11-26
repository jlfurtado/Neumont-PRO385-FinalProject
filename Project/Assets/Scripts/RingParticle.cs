using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingParticle : MonoBehaviour {

    ParticleSystem m_particle;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<ParticleSystemRenderer>().mesh = gameObject.GetComponentInParent<MeshFilter>().mesh;
        m_particle = GetComponent<ParticleSystem>();
    }
	

    public void PlayEffect()
    {
        transform.SetParent(null);

        m_particle.Play();
        StartCoroutine(DestroyOnEnd());
    }

    IEnumerator DestroyOnEnd()
    {
        while (m_particle.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
