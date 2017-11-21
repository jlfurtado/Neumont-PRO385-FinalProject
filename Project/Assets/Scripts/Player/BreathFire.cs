using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathFire : MonoBehaviour {

    [SerializeField] private string m_fireAxis;
    [SerializeField] private ParticleSystem m_breath;
    private float m_maxEmission;

	// Use this for initialization
	void Start () {
        m_maxEmission = m_breath.emission.rateOverTimeMultiplier;
	}
	
	// Update is called once per frame
	void Update () {
        var emitter = m_breath.emission;
        Debug.Log(Input.GetAxis(m_fireAxis));
        emitter.rateOverTime = Input.GetAxis(m_fireAxis) * m_maxEmission;
	}
}
