using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDifficulty : MonoBehaviour {
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private string m_tag;

    void Awake()
    {
        GameObject difficulty = GameObject.FindGameObjectWithTag(m_tag);
        if (difficulty == null)
        {
            Instantiate(m_prefab);
        }
        else
        {
            difficulty.GetComponent<Difficulty>().Increase(1);
        }
    }
}
