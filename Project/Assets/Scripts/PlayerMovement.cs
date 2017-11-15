using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private JumpCheck m_jumpCheck;
    public float m_torqueForce;
    public float m_moveForce;
    public float m_jumpForce;

    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_jumpCheck = GetComponentInChildren<JumpCheck>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_rigidbody.angularVelocity = new Vector3(0.0f, m_torqueForce * Input.GetAxis("Horizontal"), 0.0f);
        m_rigidbody.velocity = new Vector3(transform.forward.x * m_moveForce * -Input.GetAxis("Vertical"), m_rigidbody.velocity.y, transform.forward.z * m_moveForce * -Input.GetAxis("Vertical"));
        if (m_jumpCheck.grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_rigidbody.AddForce(Vector3.up * m_jumpForce);
            }
        }
    }
}
