using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2DController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;
    public MagneticSkill mouse;
    public float maxXSpeed;
    public float maxYSpeed;

    Rigidbody2D rb;
    float moveX;
    bool isGrounded = true;
    Persistent persistent;

    private Animator playerBody;

    // Start is called before the first frame update
    void Start()
    {
        persistent = Persistent.current;
        rb = GetComponent<Rigidbody2D>();
        playerBody = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!persistent.fadeOn)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                AudioManager.instance.Play("Pulo");
            }
                

        }
        else
        {
            moveX = 0;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.95f);
        if (hit && hit.transform.CompareTag("ground"))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (!persistent.fadeOn)
        {
            Vector2 moveVelocity;
            if (moveX != 0 || isGrounded)
                moveVelocity = new Vector2(moveX * walkSpeed, rb.velocity.y);
            else
                moveVelocity = new Vector2(rb.velocity.x, rb.velocity.y);
            Vector2 magnetVelocity = (mouse.strengh * mouse.forceDirection);

            //print("magnet antes: " + magnetVelocity);
            magnetVelocity = new Vector2(Mathf.Clamp(magnetVelocity.x, -maxXSpeed, maxXSpeed), Mathf.Clamp(magnetVelocity.y, -maxYSpeed, maxYSpeed));
            //print("magnet depois: " + magnetVelocity);

            rb.velocity = (moveVelocity + magnetVelocity);
            playerBody.SetFloat("walkOn", Mathf.Abs(moveX));

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
