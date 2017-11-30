using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnSFX : MonoBehaviour {

    private BreathFire m_breathFire;
    private AudioSource m_sfx;

	// Use this for initialization
	void Start () {
        m_breathFire = gameObject.GetComponentInParent<BreathFire>();
        m_sfx = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_breathFire.IsBreathingFire() && !m_sfx.isPlaying)
        {
            m_sfx.Play();
        }
        else if(m_sfx.isPlaying && !m_breathFire.IsBreathingFire())
        {
            m_sfx.Stop();
        }
	}
}
