using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathFire : MonoBehaviour {

    [SerializeField] private string m_fireAxis;
    [SerializeField] private ParticleSystem m_breath;
    private float m_maxEmission;
    private bool m_isBreathing;

    public bool IsBreathingFire()
    {
        return m_isBreathing;
    }

	// Use this for initialization
	void Start () {
        m_maxEmission = m_breath.emission.rateOverTimeMultiplier;
	}
	
	// Update is called once per frame
	void Update () {
        var emitter = m_breath.emission;
        emitter.rateOverTime = Input.GetAxis(m_fireAxis) * m_maxEmission;
        m_isBreathing = Input.GetAxis(m_fireAxis) != 0.0f;
	}
}
