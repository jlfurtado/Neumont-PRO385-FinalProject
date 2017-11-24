using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomly3D : MonoBehaviour {
    [SerializeField] private GameObject[] m_prefabs;
    [SerializeField] private Vector3 m_minSpawn;
    [SerializeField] private Vector3 m_maxSpawn;
    [SerializeField] private int m_boxesPerX;
    [SerializeField] private int m_boxesPerY;
    [SerializeField] private int m_boxesPerZ;
    [SerializeField] private float m_scale;
    [SerializeField] [Range(0.0f, 1.0f)] private float m_shrinkBox = 1.0f;
    [SerializeField] private bool m_isMesh;
    private static System.Random rand = new System.Random();

	// Use this for initialization
	void Awake () {
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

            GameObject made = HelperFuncs.MakeAt(m_prefabs[rand.Next(0, m_prefabs.Length)], HelperFuncs.RandVec(m_minSpawn + lowOffset + xyzDelta, m_minSpawn + highOffset + xyzDelta), m_scale, gameObject, "RandomPlacement|" + i);
            System.Type t = m_isMesh ? typeof(MeshCollider) : typeof(CapsuleCollider);
            made.transform.GetChild(0).gameObject.AddComponent(t);
        }
    }

}
