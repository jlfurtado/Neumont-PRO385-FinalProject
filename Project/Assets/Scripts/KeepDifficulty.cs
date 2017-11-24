using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMe : MonoBehaviour {
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private string m_tag;

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag(m_tag) == null)
        {
            Instantiate(m_prefab);
        }
    }
}
