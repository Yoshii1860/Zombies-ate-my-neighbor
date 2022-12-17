using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Scene currentScene;
    LoadGame loadGame;
    SaveGame saveGame;
    bool sameScene = false;

    void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() 
    {
        currentScene = SceneManager.GetActiveScene();
        int nextScene = currentScene.buildIndex + 1;
        saveGame = FindObjectOfType<SaveGame>();
        Transform player = FindObjectOfType<PlayerHealth>().transform;
        Vector3 playerPosition = player.position;
        saveGame.SetPlayerPosition(playerPosition);
    }

    public void ReloadGame()
    {
        sameScene = true;
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

    public void AsyncLoad()
    {
        AsyncOperation LoadSceneAsync(int nextScene, SceneManagement.LoadSceneMode mode = LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(sameScene)
        {
            loadGame = FindObjectOfType<LoadGame>();
            loadGame.enabled = true;
            loadGame.LoadPlayerPosition();
        }
    }
}
