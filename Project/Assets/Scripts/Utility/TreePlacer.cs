using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacer : MonoBehaviour {
    [SerializeField] private GameObject[] m_prefabs;
    [SerializeField] private int m_spawnCount;
    [SerializeField] private Vector3 m_minSpawn;
    [SerializeField] private Vector3 m_maxSpawn;
    [SerializeField] private int m_boxesPerDir;
    [SerializeField] private float m_scale;
    [SerializeField] [Range(0.0f, 1.0f)] private float m_shrinkBox = 1.0f;
    [SerializeField] private float m_fireAmount;
    [SerializeField] private float m_burnoutDuration;
    [SerializeField] private GameObject m_particlePrefab;
    private static System.Random rand = new System.Random();

	void Awake () {
        Vector3 delta = (m_maxSpawn - m_minSpawn) / m_boxesPerDir;
        float rmv = ((1.0f - m_shrinkBox) / 2.0f);
        Vector3 lowOffset = delta * rmv;
        Vector3 highOffset = delta - lowOffset;
        for (int i = 0; i < m_spawnCount; ++i)
        {
            int xLoc = i % m_boxesPerDir;
            int zLoc = i / m_boxesPerDir;
            Vector3 xyzDelta = new Vector3(xLoc * delta.x, delta.y, zLoc * delta.z);

            GameObject made = HelperFuncs.MakeAt(m_prefabs[rand.Next(0, m_prefabs.Length)], HelperFuncs.RandVec(m_minSpawn + lowOffset + xyzDelta, m_minSpawn + highOffset + xyzDelta), m_scale, gameObject, "RandomPlacement|" + i);
            GameObject g = made.transform.GetChild(0).gameObject;
            g.AddComponent<CapsuleCollider>();
            GameObject spawnedParticles = Instantiate(m_particlePrefab);
            spawnedParticles.transform.SetParent(g.transform);
            spawnedParticles.transform.localPosition = Vector3.zero;
            BurnableScript bs = g.AddComponent<BurnableScript>();
            bs.m_burnOutDuration = m_burnoutDuration;
            bs.m_fireAmount = m_fireAmount;
            g.tag = "Tree";
            g.layer = LayerMask.NameToLayer("Tree");
        }
    }

}
