using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject OptionMenu;

    public void PlayGame()
    {
        Debug.Log("LoadSceneByName called with sceneName: " + "sceneName");
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSetting()
    {
        StartMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        StartMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("LoadSceneByName called with sceneName: " + "sceneName");
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
