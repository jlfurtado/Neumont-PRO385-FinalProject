using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTwoPoints : MonoBehaviour {
    [SerializeField] private GameObject m_obj1;
    [SerializeField] private GameObject m_obj2;
    [SerializeField] private Vector3 m_offset;
    [SerializeField] private float m_lowDist;
    [SerializeField] private float m_highDist;
    [SerializeField] private Color m_lowColor;
    [SerializeField] private Color m_highColor;

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

        // if player going away from other player AND player distance > thresholdDistance
        if (show)
        {
            float interp = (toObj2.magnitude - m_lowDist) / (m_highDist - m_lowDist);
            transform.position = (p1 + p2) / 2.0f;
            transform.rotation = Quaternion.LookRotation(Vector3.Cross(toObj2.normalized, Vector3.up), toObj2.normalized);
            transform.localScale = new Vector3(transform.localScale.x, ((toObj2.magnitude - 1.0f) * 0.49f), transform.localScale.z);
            m_renderer.material.color = Color.Lerp(m_lowColor, m_highColor, interp);
        }
    }
}
