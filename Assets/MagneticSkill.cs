using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticSkill : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public float strengh;
    public float xForce;
    public float yForce;
    public float maxMagnetDistance;
    public LayerMask magnetsLayer;
    [HideInInspector]public Vector2 forceDirection;

    [SerializeField] private SpriteRenderer staffRenderer;
    [SerializeField] private Sprite blueStaff;
    [SerializeField] private Sprite redStaff;

    private Polarity playerPolarity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        if (Input.GetMouseButton(0))
        {
            playerPolarity = Polarity.negative;
            staffRenderer.sprite = redStaff;
            TurnMagnetOn();
        }
        else if (Input.GetMouseButton(1))
        {
            playerPolarity = Polarity.positive;
            staffRenderer.sprite = blueStaff;
            TurnMagnetOn();
        }
        else
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
            forceDirection = Vector2.zero;
        }
            

    }

    public void TurnMagnetOn()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, transform.position - player.transform.position, maxMagnetDistance, magnetsLayer);
        Debug.DrawRay(player.transform.position, transform.position - player.transform.position);
        if (hit)
        {
            Vector2 destinationDirection = hit.point - new Vector2(player.transform.position.x, player.transform.position.y);
            Polarity magnetPolarity = hit.rigidbody.gameObject.GetComponent<Magnet>().polarity;
            if (magnetPolarity == playerPolarity)
            {
                forceDirection = -destinationDirection.normalized;
                Debug.Log("afasta");
            }
            else
            {
                forceDirection = destinationDirection.normalized;
                Debug.Log("atrai");
            }
            forceDirection = new Vector2(forceDirection.x * xForce, forceDirection.y * yForce).normalized;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
            forceDirection = Vector2.zero;
        }
    }

}
