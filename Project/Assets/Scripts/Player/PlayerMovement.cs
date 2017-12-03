using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    [SerializeField] private CapstoneGenerator m_dargonGenerator;
    [SerializeField] private float m_torqueForce;
    [SerializeField] private float m_moveForce;
    [SerializeField] private float m_jumpForce;
    [SerializeField, Range(0.0f, 1.0f)] float m_movePenaltyCap;
    [SerializeField] private string m_rotateAxis;
    [SerializeField] private string m_verticalAxis;
    [SerializeField] private string m_horizontalAxis;
    [SerializeField] private string m_jumpAxis;
    [SerializeField] private PlayerMovement m_otherPlayer;
    [SerializeField] private float m_lowPlayerDistanceThreshold;
    [SerializeField] private float m_highPlayerDistanceThreshold;
    [SerializeField] private Color m_color;
    private AudioSource m_flap;

    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_flap = GetComponent<AudioSource>();
        GameObject meshChild = m_dargonGenerator.MakeInstance();
        meshChild.transform.SetParent(this.transform);
        meshChild.transform.localPosition = Vector3.zero;
        meshChild.transform.localScale = Vector3.one;
        Renderer renderer = meshChild.GetComponent<Renderer>();
        renderer.material.color = m_color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_rigidbody.angularVelocity = new Vector3(0.0f, m_torqueForce * Input.GetAxis(m_rotateAxis), 0.0f);
        Vector3 direction = (transform.forward * Input.GetAxis(m_verticalAxis) + transform.right * Input.GetAxis(m_horizontalAxis)).normalized;
        Vector3 toOtherPlayer = (m_otherPlayer.transform.position - transform.position);

        float compensatedMove = m_moveForce * Mathf.Clamp(Vector3.Dot(-transform.forward, direction), m_movePenaltyCap, 1.0f);

        // if player going away from other player AND player distance > thresholdDistance
        if ((Vector3.Dot(direction, toOtherPlayer.normalized) < 0.0f) && toOtherPlayer.magnitude > m_lowPlayerDistanceThreshold)
        { 
            float interp = (toOtherPlayer.magnitude - m_lowPlayerDistanceThreshold) / (m_highPlayerDistanceThreshold - m_lowPlayerDistanceThreshold);
            compensatedMove = Mathf.Lerp(compensatedMove, 0, interp);
        }

        m_rigidbody.velocity = new Vector3(direction.x * compensatedMove, m_rigidbody.velocity.y, direction.z * compensatedMove);


        if (Input.GetAxis(m_jumpAxis) != 0.0f)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 0.0f, m_rigidbody.velocity.z);
            m_rigidbody.AddForce(Vector3.up * m_jumpForce);
            if(!m_flap.isPlaying)m_flap.Play();
        }
        else
        {
            m_flap.Stop();
        }
    }
}
