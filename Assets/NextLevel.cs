using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] string goToScene;
    Persistent persistentData;
    [SerializeField] GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        persistentData = Persistent.current;
        if (persistentData.fadeOn)
        {
            fade.SetActive(true);
            fade.GetComponent<Animator>().Play("scenefadeout");
            persistentData.fadeOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (persistentData.canChangeScene)
        {
            persistentData.canChangeScene = false;
            SceneManager.LoadScene(goToScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            persistentData.fadeOn = true;
            fade.SetActive(true);
            fade.GetComponent<Animator>().Play("scenefadein");
        }
    }
}
