using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour {
    private int m_difficulty = 0;

    public int GetDifficulty()
    {
        return m_difficulty;
    }

    private void OnDestroy()
    {
        m_difficulty = 0;
    }

    public void Increase(int amount)
    {
        m_difficulty += amount;
    }
}
