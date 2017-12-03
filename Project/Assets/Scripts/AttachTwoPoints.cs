using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTwoPoints : MonoBehaviour {
    [SerializeField] private GameObject m_obj1;
    [SerializeField] private GameObject m_obj2;
    [SerializeField] private ParticleSystem m_tetherParticle;
    [SerializeField] private Vector3 m_offset;
    [SerializeField] private float m_lowDist;
    [SerializeField] private float m_highDist;
    [SerializeField] private Color m_lowColor;
    [SerializeField] private Color m_highColor;
    [SerializeField] private float m_modEmit;
    [SerializeField] private float m_baseEmit;

    private Renderer m_renderer;

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update () {
        Vector3 p1 = m_obj1.transform.position + m_offset;
        Vector3 p2 = m_obj2.transform.position + m_offset;

        Vector3 toObj2 = p2 - p1;

        bool show = toObj2.magnitude > m_lowDist;
        m_renderer.enabled = show;
        m_tetherParticle.gameObject.SetActive(show);
        // if player going away from other player AND player distance > thresholdDistance
        if (show)
        {
            float interp = (toObj2.magnitude - m_lowDist) / (m_highDist - m_lowDist);
            transform.position = (p1 + p2) / 2.0f;
            Quaternion interpRot = Quaternion.LookRotation(Vector3.Cross(toObj2.normalized, Vector3.up), toObj2.normalized);
            transform.rotation = interpRot;
            transform.localScale = new Vector3(transform.localScale.x, ((toObj2.magnitude - 1.0f) * 0.49f), transform.localScale.z);
            Color interpColor = Color.Lerp(m_lowColor, m_highColor, interp);
            m_renderer.material.color = interpColor;
            var main = m_tetherParticle.main;
            main.startColor = new Color(interpColor.r, interpColor.g, interpColor.b, 1.0f);
            var emit = m_tetherParticle.emission;
            emit.rateOverTime = (interp * m_modEmit) + m_baseEmit;
            var shape = m_tetherParticle.shape;
            shape.radius = toObj2.magnitude / 2.0f;
        }
    }
}
