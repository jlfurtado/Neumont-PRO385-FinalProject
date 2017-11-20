using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomly : MonoBehaviour {
    [SerializeField] private GameObject[] m_prefabs;
    [SerializeField] private int m_spawnCount;
    [SerializeField] private Vector3 m_minSpawn;
    [SerializeField] private Vector3 m_maxSpawn;
    [SerializeField] private float m_scale;
    [SerializeField] private bool m_isMesh;
    private static System.Random rand = new System.Random();

	// Use this for initialization
	void Awake () {
	    for (int i = 0; i < m_spawnCount; ++i)
        {
            GameObject made = HelperFuncs.MakeAt(m_prefabs[rand.Next(0, m_prefabs.Length)], HelperFuncs.RandVec(m_minSpawn, m_maxSpawn), m_scale, gameObject, "RandomPlacement|" + i);
            System.Type t = m_isMesh ? typeof(MeshCollider) : typeof(CapsuleCollider);
            made.transform.GetChild(0).gameObject.AddComponent(t);
        }
    }

}
