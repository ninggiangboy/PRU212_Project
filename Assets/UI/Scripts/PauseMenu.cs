using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject OptionMenu;

    public bool isPause { get; set; }

    public void Start()
    {
        isPause = false;
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("dfs");
        isPause = false;
        Debug.Log(isPause);
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("sdfds");
        isPause = true;
    }

    public void OpenSetting()
    {
        PauseMenuUI.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        PauseMenuUI.SetActive(true);
        OptionMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("LoadSceneByName called with sceneName: " + "sceneName");
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
