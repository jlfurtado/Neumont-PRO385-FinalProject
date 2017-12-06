using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalVarier : MonoBehaviour {
    [SerializeField] private float m_xSeed;
    [SerializeField] private float m_ySeed;
    [SerializeField] private float m_xDelta;
    [SerializeField] private float m_yDelta;
    [SerializeField] private float m_xMax;
    [SerializeField] private float m_yMax;
    [SerializeField] private float m_xMin;
    [SerializeField] private float m_yMin;
    private Renderer m_myRenderer;

    // Use this for initialization
    void Awake () {
        m_myRenderer = GetComponentInChildren<Renderer>();

    }

    // Update is called once per frame
    void Update () {
        m_xSeed += m_xDelta; if (m_xSeed > m_xMax || m_xSeed < m_xMin) { m_xDelta *= -1.0f; }
        m_ySeed += m_yDelta; if (m_ySeed > m_yMax || m_ySeed < m_yMin) { m_yDelta *= -1.0f; }

        m_myRenderer.material.SetFloat("_myRVX", m_xSeed);
        m_myRenderer.material.SetFloat("_myRVY", m_ySeed);

    }
}
