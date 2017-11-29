using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDifficulty : MonoBehaviour {
    [SerializeField] private string m_difficultyTag;
    [SerializeField] private Text m_objectiveText;
    [SerializeField] private Text m_timeText;
    [SerializeField] private Text m_scoreText;

    private void Start()
    {
        Difficulty difficulty = GameObject.FindGameObjectWithTag(m_difficultyTag).GetComponent<Difficulty>();
        difficulty.SetText(m_timeText, m_objectiveText, m_scoreText);
    }
}
