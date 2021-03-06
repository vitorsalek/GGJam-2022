using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SettingsUI;
    public GameObject CreditsUI;

    public void LoadPlay()
    {
        //AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene("Nivel_1-3");

    }

    public void LoadSpecialLevel()
    {
        //AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene("Nivel_7");

    }

    public void LoadSettings()
    {
        //AudioManager.instance.Play("ButtonClick");
        SettingsUI.SetActive(true);
    }
    public void QuitSettings()
    {
        //AudioManager.instance.Play("ButtonClick");
        SettingsUI.SetActive(false);
    }

    public void LoadCredits()
    {
        //AudioManager.instance.Play("ButtonClick");
        MainMenuUI.SetActive(false);
        CreditsUI.SetActive(true);
    }

    public void QuitCredits()
    {
        //AudioManager.instance.Play("ButtonClick");
        CreditsUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
