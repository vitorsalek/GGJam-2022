using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public Image fadeImage;
    Vector3 lastRespawnPosition;

    private void Start()
    {
        lastRespawnPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damage"))
        {
            fadeImage.gameObject.SetActive(true);
            Persistent.current.fadeOn = true;
            fadeImage.gameObject.GetComponent<Animator>().Play("fadein");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("checkpoint"))
        {
            lastRespawnPosition = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
        }
    }

    public void ResetPlayerPos()
    {
        transform.position = lastRespawnPosition;
    }
    /* public IEnumerator FadeImageDeath()
     {
         fadeImage.gameObject.SetActive(true);

         // fade from transparent to black
         fadeImage.color = new Color(0, 0, 0, 0);
         for (float i = 0; i <= 1; i += Time.deltaTime)
         {
             fadeImage.color = new Color(0, 0, 0, i);
             yield return null;
         }

         transform.position = lastRespawnPosition;

         fadeImage.color = new Color(0, 0, 0, 1);
         for (float i = 1; i >= 0; i -= Time.deltaTime)
         {
             fadeImage.color = new Color(0, 0, 0, i);
             yield return null;
         }

         fadeImage.gameObject.SetActive(false);
     }*/
}
