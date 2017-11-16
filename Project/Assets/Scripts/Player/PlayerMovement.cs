using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private JumpCheck m_jumpCheck;
    public float m_torqueForce;
    public float m_moveForce;
    public float m_jumpForce;
    [SerializeField, Range(0.0f, 1.0f)] float m_movePenaltyCap;
    [SerializeField] private string m_rotateAxis;
    [SerializeField] private string m_verticalAxis;
    [SerializeField] private string m_horizontalAxis;
    [SerializeField] private string m_jumpAxis;

    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_jumpCheck = GetComponentInChildren<JumpCheck>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_rigidbody.angularVelocity = new Vector3(0.0f, m_torqueForce * Input.GetAxis(m_rotateAxis), 0.0f);
        Vector3 direction = (transform.forward * Input.GetAxis(m_verticalAxis) + transform.right * Input.GetAxis(m_horizontalAxis)).normalized;
        float compensatedMove = m_moveForce * Mathf.Clamp(Vector3.Dot(-transform.forward, direction), m_movePenaltyCap, 1.0f);
        m_rigidbody.velocity = new Vector3(direction.x * compensatedMove, m_rigidbody.velocity.y, direction.z * compensatedMove);

        if (m_jumpCheck.grounded)
        {
            if (Input.GetAxis(m_jumpAxis) != 0.0f)
            {
                m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 0.0f, m_rigidbody.velocity.z);
                m_rigidbody.AddForce(Vector3.up * m_jumpForce);
            }
        }
    }
}
