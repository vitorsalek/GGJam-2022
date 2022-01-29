using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Vector3 lastRespawnPosition;

    private void Start()
    {
        lastRespawnPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damage"))
        {
            transform.position = lastRespawnPosition;
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
}
