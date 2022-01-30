using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Persistent.current.timer = 0f;
    }

}
