using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static int StoreScore;
    private static string PlayerName = Strings.DEFAULT_NAME;
    public Text scoreText;
    private int scoreValue = 0;
    private string[] highScores;

    public static void SetName(string name)
    {
        PlayerName = name == "" ? Strings.DEFAULT_NAME : name;
    }

    public static string GetName()
    {
        return PlayerName;
    }

    void Awake()
    {
        highScores = new string[Strings.HIGH_SCORE_KEYS.Length];

        for (int i = 0; i < highScores.Length; ++i)
        {
            highScores[i] = PlayerPrefs.GetString(Strings.HIGH_SCORE_KEYS[i], Strings.DEFAULT_SCORE_TEXT);
        }

        SetScoreText();
    }

    public void AddScore(int value)
    {
        scoreValue += value;
        SetScoreText();
    }

    public void EnsureTextUpdated()
    {
        SetScoreText();
        StoreScore = scoreValue;
    }

    private void SetScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = string.Concat(Strings.SCORE_PREFIX, scoreValue);
        }
    }

    void OnDestroy()
    {
        for (int i = 0; i < highScores.Length; ++i)
        {
            if (scoreValue > int.Parse(highScores[i].Substring(highScores[i].IndexOf(':') + 1)))
            {
                for (int j = highScores.Length - 2; j >= i; --j)
                {
                    highScores[j + 1] = highScores[j];
                }

                highScores[i] = string.Concat(PlayerName, Strings.COLON, scoreValue);
                break;
            }
        }

        for (int i = 0; i < highScores.Length; ++i)
        {
            PlayerPrefs.SetString(Strings.HIGH_SCORE_KEYS[i], highScores[i]);
        }

        PlayerPrefs.Save();
    }
}
