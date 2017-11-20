using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour {

	public void MoveToGameScene()
    {
        SceneManager.LoadScene(Scenes.GAME);
    }

    public void MoveToTitle()
    {
        SceneManager.LoadScene(Scenes.TITLE);
    }

    public void MoveToVictory()
    {
        SceneManager.LoadScene(Scenes.VICTORY);
    }

    public void MoveToGameOver()
    {
        SceneManager.LoadScene(Scenes.GAME_OVER);
    }
}
