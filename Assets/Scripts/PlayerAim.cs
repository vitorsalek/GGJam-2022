using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    private Vector3 mousePos;
    SpriteRenderer playerSprite;

    private void Awake()
    {
        
    }

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        playerSprite.flipX = aimDir.x < 0;
    }
}
