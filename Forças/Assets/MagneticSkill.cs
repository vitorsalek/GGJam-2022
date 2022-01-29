using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticSkill : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public float strengh;
    public float xForce;
    public LayerMask magnetsLayer;
    [HideInInspector]public Vector2 forceDirection;

    private Polarity playerPolarity;
    private bool mousePressed;
    // Start is called before the first frame update
    void Start()
    {
        playerPolarity = Polarity.negative;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            mousePressed = true;
            Vector2 destinationDirection = transform.position - player.transform.position;
            Ray2D ray = new Ray2D(player.transform.position, destinationDirection);
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, destinationDirection, 200f, magnetsLayer);
            Debug.DrawRay(player.transform.position, destinationDirection);
            if (hit)
            {
                Polarity magnetPolarity = hit.rigidbody.gameObject.GetComponent<Magnet>().polarity;
                Debug.Log("encontrou");
                if (magnetPolarity == playerPolarity)
                {
                    //PushPlayer(destinationDirection);
                    forceDirection = -destinationDirection.normalized;
                    Debug.Log("afasta");
                }

                else
                {
                    //PullPlayer(destinationDirection);
                    forceDirection = destinationDirection.normalized;
                    Debug.Log("atrai");
                }
                forceDirection = new Vector2(forceDirection.x * xForce, forceDirection.y).normalized;

            }
            else
                forceDirection = Vector2.zero;
        }
        else
            forceDirection = Vector2.zero;

    }


    public void PushPlayer(Vector2 direction)
    {
        Vector2 force = -direction.normalized;
        player.transform.position += new Vector3(force.x, force.y, 0f) * strengh;
        //player.GetComponent<Rigidbody2D>().AddForce(force * forceStrengh);
    }

    public void PullPlayer(Vector2 direction)
    {
        //player.GetComponent<Rigidbody2D>().AddForce(force * forceStrengh, ForceMode2D.Force);
        //player.transform.position += new Vector3(force.x, force.y, 0f)*strengh;
        player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity + direction.normalized * strengh;

        //player.GetComponent<Rigidbody2D>().AddForce(force * forceStrengh);
    }
}
