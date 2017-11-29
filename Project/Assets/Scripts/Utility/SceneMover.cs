using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMover : MonoBehaviour {

	public void MoveToGameScene()
    {
        KeepTagged("Difficulty");
        SetPlayerName();
        SceneManager.LoadScene(Scenes.GAME);
    }

    public void MoveToTitle()
    {
        DontKeepTagged("Difficulty");
        SceneManager.LoadScene(Scenes.TITLE);
    }

    public void MoveToOptions()
    {
        DontKeepTagged("Difficulty");
        SceneManager.LoadScene(Scenes.OPTIONS);
    }

    public void MoveToGameOver()
    {
        KeepTagged("Difficulty");
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
        GameObject inputObj = GameObject.FindGameObjectWithTag(Strings.PLAYER_NAME_INPUT_TAG);

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
