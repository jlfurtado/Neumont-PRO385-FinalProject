using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour {
    [SerializeField] private float m_timeLimit = 60.0f;
    [SerializeField] private int m_objectsPerDifficulty = 5;
    [SerializeField] private int m_minObjective = 5;
    [SerializeField] private float m_ringShakeDuration = 1.0f;
    [SerializeField] private float m_ringShakeIntensity = 1.0f;

    private ScoreManager m_scoreManager;
    private SceneMover m_sceneMover;
    private Text m_timeText;
    private Text m_objectiveText;
    private int m_difficulty = 0;
    private int m_treesNeeded = 0;
    private int m_ringsNeeded = 0;
    private float m_timeRemaining = 0.0f;
    private static System.Random rand = new System.Random();
    private bool m_timing = false;
    private FollowCenter m_camera;

    public void SetText(Text time, Text objective, Text score)
    {
        m_timeText = time;
        m_objectiveText = objective;
        m_scoreManager.scoreText = score;
        m_scoreManager.EnsureTextUpdated();
        SetTimeText();
        SetObjectiveText();
    }

    private void Awake()
    {
        m_camera = Camera.main.GetComponent<FollowCenter>();
        m_sceneMover = GetComponent<SceneMover>();
        m_scoreManager = GetComponent<ScoreManager>();
    }

    private void Start()
    {
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
        m_scoreManager.AddScore(amount);
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
            Destroy(m_scoreManager.gameObject);
            m_sceneMover.MoveToGameOver();
        }
    }

    private void CreateWinConditionFromDifficulty()
    {
        int m_total = m_minObjective + m_difficulty * m_objectsPerDifficulty;
        int m_minTrees = Mathf.Min((m_total / 2) + 1, m_total - 1);
        m_treesNeeded = m_minTrees + rand.Next(0, m_total - m_minTrees);
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
        m_camera.AddShake(m_ringShakeDuration, m_ringShakeIntensity);

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
