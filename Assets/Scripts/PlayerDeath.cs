using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerDeath : MonoBehaviour
{
    public Image fadeImage;
    public float timeUntilFade;
    Vector3 lastRespawnPosition;

    private void Start()
    {
        if (Persistent.current.fadeOn)
            fadeImage.gameObject.GetComponent<Animator>().Play("fadeout");
        lastRespawnPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damage"))
        {
            GetComponent<PlayerAnimation>().DeathAnimation();
            AudioManager.instance.Play("Death");
            Persistent.current.fadeOn = true;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(AfterDeathEvents());
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
        GetComponent<PlayerAnimation>().DestroyDeathRemains();
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Light2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        transform.position = lastRespawnPosition;
    }

    public IEnumerator AfterDeathEvents()
    {
        yield return new WaitForSeconds(timeUntilFade);
        fadeImage.gameObject.SetActive(true);
        fadeImage.gameObject.GetComponent<Animator>().Play("fadein");
    }
}
