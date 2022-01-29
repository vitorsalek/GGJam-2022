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
    bool isGrounded=true;
    Persistent persistent;

    // Start is called before the first frame update
    void Start()
    {
        persistent = Persistent.current;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!persistent.fadeOn)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (!persistent.fadeOn)
        {
            Vector2 moveVelocity = new Vector2(moveX * speed, rb.velocity.y);
            Vector2 magnetVelocity = (mouse.strengh * mouse.forceDirection);

            //print("magnet antes: " + magnetVelocity);
            magnetVelocity = new Vector2(magnetVelocity.x, Mathf.Clamp(magnetVelocity.y, -0.8f, 0.8f));
            //print("magnet depois: " + magnetVelocity);

            rb.velocity = (moveVelocity + magnetVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            isGrounded = false;
    }
}
