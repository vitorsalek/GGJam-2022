using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour
{
    public static Persistent current;

    [HideInInspector] public bool fadeOn;
    [HideInInspector] public bool canChangeScene;

    [HideInInspector] public float timer;

    private void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("persistentData");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        if (current == null)
        {
            current = this;
        }
    }
}
