using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject settingsUI;

    private bool _paused;

    private void Start()
    {
        _paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
            Pause();           
    }
    public void Pause()
    {
        //AudioManager.instance.Play("ButtonClick");
        if (_paused)
        {
            _paused = false;
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            return;
        }
        else
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            _paused = true;
        }
    }

    public void BackToGame()
    {
        //AudioManager.instance.Play("ButtonClick");
        pauseMenuUI.SetActive(false);
        _paused = false;
        Time.timeScale = 1f;
    }

    public void GoToSettings()
    {
        //AudioManager.instance.Play("ButtonClick");
        settingsUI.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        //AudioManager.instance.Play("ButtonClick");
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(true);
        settingsUI.SetActive(false);

    }
    public void GoToMainMenu()
    {
        //AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
