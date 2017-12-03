using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacer : MonoBehaviour {
    [SerializeField] private CapstoneGenerator m_generator;
    [SerializeField] private Vector3 m_minSpawn;
    [SerializeField] private Vector3 m_maxSpawn;
    [SerializeField] private int m_boxesPerX;
    [SerializeField] private int m_boxesPerY;
    [SerializeField] private int m_boxesPerZ;
    [SerializeField] [Range(0.0f, 1.0f)] private float m_shrinkBox = 1.0f;
    [SerializeField] private float m_fireAmount;
    [SerializeField] private float m_burnoutDuration;
    [SerializeField] private GameObject m_particlePrefab;

    // Use this for initialization
    void Start()
    {
        int spawnCount = m_boxesPerX * m_boxesPerY * m_boxesPerZ;
        Vector3 delta = (m_maxSpawn - m_minSpawn);
        delta = new Vector3(delta.x / m_boxesPerX, delta.y / m_boxesPerY, delta.z / m_boxesPerZ);
        float rmv = ((1.0f - m_shrinkBox) / 2.0f);
        Vector3 lowOffset = delta * rmv;
        Vector3 highOffset = delta - lowOffset;
        for (int i = 0; i < spawnCount; ++i)
        {
            int xLoc = i % m_boxesPerX;
            int yLoc = (i / m_boxesPerX) % m_boxesPerY;
            int zLoc = i / (m_boxesPerX * m_boxesPerY);
            Vector3 xyzDelta = new Vector3(xLoc * delta.x, yLoc * delta.y, zLoc * delta.z);

            GameObject made = MakeAt(RandVec(m_minSpawn + lowOffset + xyzDelta, m_minSpawn + highOffset + xyzDelta), gameObject, "RandomPlacement|" + i);
            made.AddComponent<CapsuleCollider>();
            GameObject spawnedParticles = Instantiate(m_particlePrefab);
            spawnedParticles.transform.SetParent(made.transform);
            spawnedParticles.transform.localPosition = Vector3.zero;
            spawnedParticles.transform.localScale = Vector3.one;
            BurnableScript bs = made.AddComponent<BurnableScript>();
            bs.m_burnOutDuration = m_burnoutDuration;
            bs.m_fireAmount = m_fireAmount;
            made.tag = "Tree";
            made.layer = LayerMask.NameToLayer("Tree");
        }
    }

    private GameObject MakeAt(Vector3 position, GameObject parent, string name)
    {
        GameObject created = m_generator.MakeInstance();
        created.transform.SetParent(parent.transform);
        created.transform.position = Vector3.zero;
        created.transform.localPosition = position;
        created.name = name;
        return created;
    }

    public static Vector3 RandVec(Vector3 min, Vector3 max)
    {
        return new Vector3(Random.Range(min.x, max.x),
                           Random.Range(min.y, max.y),
                           Random.Range(min.z, max.z));
    }

}
