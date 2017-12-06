using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableScript : MonoBehaviour {

    public float m_fireAmount;
    public float m_burnOutDuration;
    private float m_currentFireAmount;
    private bool m_isBurningOut;
    private float m_maxBurn;
    private ParticleSystem m_burn;
    private Vector3 m_maxScale;
    private bool m_beingBurnt;
    private float m_lowestBurn;
    private Difficulty m_difficulty;

    public void Burning()
    {
        m_beingBurnt = true;
    }

	// Use this for initialization
	void Start () {
        m_isBurningOut = false;
        m_burn = GetComponentInChildren<ParticleSystem>();
        m_maxBurn = m_burn.emission.rateOverTimeMultiplier;
        m_maxScale = transform.localScale;
        m_lowestBurn = 0.0f;
        m_difficulty = GameObject.FindGameObjectWithTag("Difficulty").GetComponent<Difficulty>();
    }

    public bool IsMaxFire()
    {
        return m_currentFireAmount >= m_fireAmount;
    }

    public void AddFire(float amount)
    {
        m_currentFireAmount += amount;
    }
	
	// Update is called once per frame
	void Update () {
        float percentBurnt = (m_currentFireAmount / m_fireAmount);
        m_lowestBurn = percentBurnt > m_lowestBurn ? percentBurnt : m_lowestBurn;
        if (percentBurnt > 0.5f && !IsMaxFire())
        {
            m_currentFireAmount += Time.deltaTime * percentBurnt;
        }
        else if(m_currentFireAmount > 0.0f)
        {
            m_currentFireAmount -= Time.deltaTime * (1 - percentBurnt) * (m_beingBurnt ? 0.1f : 1.0f);
        }
        if (IsMaxFire() && !m_isBurningOut)
        {
            m_isBurningOut = true;
            StartCoroutine(BurnOut());
        }
        var emission = m_burn.emission;
        emission.rateOverTimeMultiplier = percentBurnt * m_maxBurn;
        if ((2 - (m_lowestBurn * 1.5f)) < 1.0f)
        {
            var main = m_burn.main;
            main.scalingMode = ParticleSystemScalingMode.Local;
            main.startSpeed = 60 * (2 - (m_lowestBurn * 1.5f));
        }
        transform.localScale = m_maxScale * (2 - (m_lowestBurn * 1.5f));
        m_beingBurnt = false;
	}

    IEnumerator BurnOut()
    {
        yield return new WaitForSeconds(m_burnOutDuration);
        m_difficulty.BurnTree();
        gameObject.SetActive(false);
    }
}
