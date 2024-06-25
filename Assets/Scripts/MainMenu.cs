using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void PlayGame()
    {
        Debug.Log("LoadSceneByName called with sceneName: " + "sceneName");
        SceneManager.LoadScene("SampleScene");
    }

    //public void GoToSettingsMenu()
    //{
    //    SceneManager.LoadScene("SettingsMenu");
    //}

    //public void GoToMainMenu()
    //{
    //    SceneManager.LoadScene("StartMenu");
    //}

    public void QuitGame()
    {
        Debug.Log("LoadSceneByName called with sceneName: " + "sceneName");
        EditorApplication.isPlaying = false;
        Application.Quit();
       
    }
    

}
