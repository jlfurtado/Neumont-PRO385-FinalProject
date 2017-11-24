using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDifficulty : MonoBehaviour {
    [SerializeField] private string m_difficultyTag;
    [SerializeField] private Text m_test;

    private void Start()
    {
        Difficulty difficulty = GameObject.FindGameObjectWithTag(m_difficultyTag).GetComponent<Difficulty>();
        m_test.text = "D: " + difficulty.GetDifficulty();
    }
}
