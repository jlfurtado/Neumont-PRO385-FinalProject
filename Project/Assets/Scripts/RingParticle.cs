using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingParticle : MonoBehaviour {

    private ParticleSystem m_particle;
    private AudioSource m_ringCollectSFX;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<ParticleSystemRenderer>().mesh = gameObject.GetComponentInParent<MeshFilter>().mesh;
        m_particle = GetComponent<ParticleSystem>();
        m_ringCollectSFX = GetComponent<AudioSource>();
    }
	

    public void PlayEffect()
    {
        transform.SetParent(null);
        m_ringCollectSFX.Play();
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
