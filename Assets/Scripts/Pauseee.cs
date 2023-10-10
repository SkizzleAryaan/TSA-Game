using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pauseee : MonoBehaviour
{
    public static bool GamePaused = false;
    GameObject pausemenuUI;
    GameObject helpmenuUI; 


    private void Awake()
    {
        pausemenuUI = GameObject.Find("PauseMenu");
        pausemenuUI.SetActive(false);

        helpmenuUI = GameObject.Find("HelpPanel");
        helpmenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;

        GamePaused = false;
    }

    public void Pause()
    {
        pausemenuUI.SetActive(true);
        helpmenuUI.SetActive(false);

        Time.timeScale = 0f;

        GamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.FindWithTag("audioPlayer"));
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadHub()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.FindWithTag("audioPlayer"));
        SceneManager.LoadScene("MainHub");
        pausemenuUI.SetActive(false);
    }

    public void Controls()
    {
        helpmenuUI.SetActive(true);
        pausemenuUI.SetActive(false);
    }

    public void CloseHelp()
    {
        helpmenuUI.SetActive(false);
        pausemenuUI.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}


