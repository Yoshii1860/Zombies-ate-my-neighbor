using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Scene currentScene;

    void Start() 
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void ReloadGame()
    {
        //if Json exists, load, if not:
        SceneManager.LoadScene(currentScene.buildIndex);
        Time.timeScale = 1;
    }

    public void NextScene()
    {
       SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
