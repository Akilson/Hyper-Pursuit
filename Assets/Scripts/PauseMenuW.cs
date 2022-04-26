using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuW : MonoBehaviour
{
    public static bool GameIsPausedd = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPausedd)
            {
                Debug.Log("unpaused");
                Resume();
            }
            else
            {
                Debug.Log("paused");
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPausedd = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPausedd = true;
    }

    public void LauchSettings()
    {
        //SceneManager.LoadScene();
    }

    //public void LoadMenu()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("Menu");
    //    Resume();
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
}
