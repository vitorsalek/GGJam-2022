using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public Image fadeImage;
    public float timeUntilFade;
    Vector3 lastRespawnPosition;

    private void Start()
    {
        lastRespawnPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damage"))
        {
            GetComponent<PlayerAnimation>().DeathAnimation();
            Persistent.current.fadeOn = true;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(PreDeathEvents());
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
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        transform.position = lastRespawnPosition;
    }

    public IEnumerator PreDeathEvents()
    {
        yield return new WaitForSeconds(timeUntilFade);
        fadeImage.gameObject.SetActive(true);
        fadeImage.gameObject.GetComponent<Animator>().Play("fadein");
    }
}
