using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimEvents : MonoBehaviour
{

    private Animator menuBGAnim;
    private PlayerDeath player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
    }
    public void DesactivateThis()
    {
        gameObject.SetActive(false);
    }

    public void MenuBGFadeInOut(int fadeIn)
    {
        if (fadeIn == 1)
            menuBGAnim.Play("BG Fadein");
        else
            menuBGAnim.Play("BG Fadeout");
    }

    public void PauseUnpauseTimeScale(int timeOn)
    {
        if (timeOn == 1)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
    }
    public void PauseUnpauseTimeVariable(int timeOn)
    {
        if (timeOn == 1)
            Persistent.current.fadeOn = false;
        else
            Persistent.current.fadeOn = true;
    }

    public void ResetPlayerPos()
    {
        player.ResetPlayerPos();
    }

    public void canChangeScene()
    {
        Persistent.current.canChangeScene = true;
    }
}
