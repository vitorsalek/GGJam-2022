using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public string musicName;
    public bool persistToNextScene;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play(musicName);
    }
    private void OnDestroy()
    {
        if (AudioManager.instance != null && !persistToNextScene)
            AudioManager.instance.StopAllMusicSounds();
    }
}
