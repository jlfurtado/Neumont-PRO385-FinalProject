using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour {

	public void MoveToGameScene()
    {
        KeepTagged("Difficulty");
        SceneManager.LoadScene(Scenes.GAME);
    }

    public void MoveToTitle()
    {
        DontKeepTagged("Difficulty");
        SceneManager.LoadScene(Scenes.TITLE);
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
}
