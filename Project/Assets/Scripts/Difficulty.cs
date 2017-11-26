using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour {
    [SerializeField] private float m_timeLimit = 60.0f;
    [SerializeField] private int m_objectsPerDifficulty = 5;
    [SerializeField] private int m_minObjective = 5;

    private SceneMover m_sceneMover;
    private Text m_timeText;
    private Text m_objectiveText;
    private int m_difficulty = 0;
    private int m_treesNeeded = 0;
    private int m_ringsNeeded = 0;
    private float m_timeRemaining = 0.0f;
    private static System.Random rand = new System.Random();
    private bool m_timing = false;

    public void SetText(Text time, Text objective)
    {
        m_timeText = time;
        m_objectiveText = objective;
        SetTimeText();
        SetObjectiveText();
    }

    private void Start()
    {
        m_sceneMover = GetComponent<SceneMover>();
        CreateWinConditionFromDifficulty();
    }

    public int GetDifficulty()
    {
        return m_difficulty;
    }

    private void OnDestroy()
    {
        m_difficulty = 0;
        CreateWinConditionFromDifficulty();
    }

    public void Increase(int amount)
    {
        m_difficulty += amount;
        CreateWinConditionFromDifficulty();
    }

    private void Update()
    {
        if (m_timing)
        {
            m_timeRemaining -= Time.deltaTime;
            SetTimeText();

            if (m_timeRemaining < 0.0f)
            {
                StopTimer();
                HandleWinCondition();
            }
        }
    }

    private void HandleWinCondition()
    {
        if (DidWin())
        {
            m_sceneMover.MoveToGameScene();
        }
        else
        {
            m_sceneMover.MoveToGameOver();
        }
    }

    private void CreateWinConditionFromDifficulty()
    {
        int m_total = m_minObjective + m_difficulty * m_objectsPerDifficulty;
        m_treesNeeded = 1 + rand.Next(0, m_total - 1);
        m_ringsNeeded = m_total - m_treesNeeded;
        StartTimer();
        SetObjectiveText();
    }

    private void StartTimer()
    {
        m_timeRemaining = m_timeLimit;
        m_timing = true;
        SetTimeText();
    }

    private void StopTimer()
    {
        m_timeRemaining = 0.0f;
        m_timing = false;
        SetTimeText();
    }

    private bool DidWin()
    {
        return (m_treesNeeded == 0) && (m_ringsNeeded == 0);
    }
    
    public void CollectRing()
    {
        m_ringsNeeded = Mathf.Max(0, m_ringsNeeded - 1);
        SetObjectiveText();

        if (DidWin())
        {
            m_sceneMover.MoveToGameScene();
        }
    }

    public void BurnTree()
    {
        m_treesNeeded = Mathf.Max(0, m_treesNeeded - 1);
        SetObjectiveText();

        if (DidWin())
        {
            m_sceneMover.MoveToGameScene();
        }
    }

    private void SetObjectiveText()
    {
        if (m_objectiveText != null)
        {
            m_objectiveText.text = "Trees: " + m_treesNeeded + "\nRings: " + m_ringsNeeded;
        }
    }

    private void SetTimeText()
    {
        if (m_timeText != null)
        {
            m_timeText.text = "Time Remaining: " + m_timeRemaining.ToString("0.0");
        }
    }
}
