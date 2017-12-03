using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RingDetection : MonoBehaviour {

    private Difficulty m_difficulty;

    private void Start()
    {
        m_difficulty = GameObject.FindGameObjectWithTag("Difficulty").GetComponent<Difficulty>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_difficulty.CollectRing();
        GetComponentInChildren<RingParticle>().PlayEffect();
        transform.parent.gameObject.SetActive(false);
    }
}
