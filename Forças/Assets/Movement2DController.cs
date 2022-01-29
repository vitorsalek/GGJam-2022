using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2DController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    Rigidbody2D rb;
    float moveX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        Vector2 moveVelocity = new Vector2(moveX * speed, rb.velocity.y);
        rb.velocity = moveVelocity;
        //rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y);
        //rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
