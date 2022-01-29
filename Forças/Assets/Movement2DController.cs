using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2DController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public MagneticSkill mouse;

    Rigidbody2D rb;
    float moveX;
    bool canJump=true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
    }

    private void FixedUpdate()
    {
        Vector2 moveVelocity = new Vector2(moveX * speed, rb.velocity.y);
        //print("moveVelocity "+moveVelocity);
        //print("forceDirection "+mouse.forceDirection*mouse.strengh);
        rb.velocity = (moveVelocity + mouse.strengh*mouse.forceDirection);
        print("soma " + (moveVelocity + mouse.strengh * mouse.forceDirection));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            canJump = true;
    }
}
