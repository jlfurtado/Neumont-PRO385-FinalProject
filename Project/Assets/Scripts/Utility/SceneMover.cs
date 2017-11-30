using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMover : MonoBehaviour {

	public void MoveToGameScene()
    {
        KeepTagged(Tags.DIFFICULTY);
        DontKeepTagged(Tags.UI_MUSIC);
        SetPlayerName();
        SceneManager.LoadScene(Scenes.GAME);
    }

    public void MoveToTitle()
    {
        DontKeepTagged(Tags.DIFFICULTY);
        KeepTagged(Tags.UI_MUSIC);
        SceneManager.LoadScene(Scenes.TITLE);
    }

    public void MoveToOptions()
    {
        DontKeepTagged(Tags.DIFFICULTY);
        KeepTagged(Tags.UI_MUSIC);
        SceneManager.LoadScene(Scenes.OPTIONS);
    }

    public void MoveToGameOver()
    {
        KeepTagged(Tags.DIFFICULTY);
        DontKeepTagged(Tags.UI_MUSIC);
        SceneManager.LoadScene(Scenes.GAME_OVER);
    }

    private void KeepTagged(string tag)
    {
        GameObject tagged = GameObject.FindGameObjectWithTag(tag);
        if (tagged != null) { DontDestroyOnLoad(tagged); }
    }

    private void DontKeepTagged(string tag)
    {
        GameObject tagged = GameObject.FindGameObjectWithTag(tag);
        if (tagged != null) { Destroy(tagged); }
    }

    private void SetPlayerName()
    {
        GameObject inputObj = GameObject.FindGameObjectWithTag(Tags.PLAYER_NAME_INPUT);

        if (inputObj != null)
        {
            InputField inputField = inputObj.GetComponent<InputField>();

            if (inputField != null)
            {
                ScoreManager.SetName(inputField.text);
            }
        }
    }
}
