using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        TimeSpan TimeSpan = TimeSpan.FromSeconds(timer);
        string output = String.Format("{0}:{1}", TimeSpan.Minutes.ToString("00"), TimeSpan.Seconds.ToString("00"));
        timerText.text = output;
    }
}
