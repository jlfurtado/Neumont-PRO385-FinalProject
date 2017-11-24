using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralWalls : MonoBehaviour {
    [SerializeField] private GameObject m_planePrefab;
    [SerializeField] private Vector3 m_minSpawn;
    [SerializeField] private Vector3 m_maxSpawn;

    const int NUM_PLANES = 6;
    const int UP = 0, DOWN = 1, LEFT = 2, RIGHT = 3, FRONT = 4, BACK = 5;
    const float DEFAULT_PLANE_SCALE = 10.0f;
    private GameObject[] m_planes = new GameObject[NUM_PLANES];

    void Awake () {
        Vector3 scale = (m_maxSpawn - m_minSpawn) / DEFAULT_PLANE_SCALE;

        for (int i = 0; i < NUM_PLANES; ++i)
        {
            m_planes[i] = Instantiate(m_planePrefab);
            m_planes[i].transform.parent = transform;
            m_planes[i].transform.position = transform.position;
            m_planes[i].transform.localPosition = Vector3.zero;
        }

        m_planes[UP].transform.rotation = Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f));
        m_planes[DOWN].transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        m_planes[LEFT].transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
        m_planes[RIGHT].transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
        m_planes[FRONT].transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
        m_planes[BACK].transform.rotation = Quaternion.Euler(new Vector3(270.0f, 0.0f, 0.0f));

        m_planes[UP].transform.localScale = scale;
        m_planes[DOWN].transform.localScale = scale;
        m_planes[LEFT].transform.localScale = new Vector3(scale.y, scale.x, scale.z); // x -> y
        m_planes[RIGHT].transform.localScale = new Vector3(scale.y, scale.x, scale.z); // x -> y
        m_planes[FRONT].transform.localScale = new Vector3(scale.x, scale.z, scale.y); // x -> z
        m_planes[BACK].transform.localScale = new Vector3(scale.x, scale.z, scale.y); // x -> z

        Vector3 transHalfScale = scale / 2.0f * DEFAULT_PLANE_SCALE;
        m_planes[UP].transform.localPosition = Vector3.up * transHalfScale.y;
        m_planes[DOWN].transform.localPosition = Vector3.down * transHalfScale.y;
        m_planes[LEFT].transform.localPosition = Vector3.left * transHalfScale.x;
        m_planes[RIGHT].transform.localPosition = Vector3.right * transHalfScale.x;
        m_planes[FRONT].transform.localPosition = Vector3.back * transHalfScale.z;
        m_planes[BACK].transform.localPosition = Vector3.forward * transHalfScale.z;

        transform.position = (m_minSpawn + m_maxSpawn) / 2.0f;
    }

}
