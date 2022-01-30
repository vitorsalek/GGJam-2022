using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MagneticSkill : MonoBehaviour
{
    public float strengh;
    public float xForce;
    public float yForce;
    public float maxMagnetDistance;
    public LayerMask magnetsLayer;
    [HideInInspector]public Vector2 forceDirection;

    private Camera cam;
    private GameObject player;
    [SerializeField] private SpriteRenderer staffRenderer;
    [SerializeField] private Sprite blueStaff;
    [SerializeField] private Sprite redStaff;
    private Animator staffPower;
    private Light2D staffLight;
    private Light2D staffRange;

    private Polarity playerPolarity;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        staffPower = staffRenderer.gameObject.transform.parent.GetChild(1).GetChild(0).GetComponent<Animator>();
        staffLight = staffRenderer.gameObject.transform.parent.GetChild(1).GetChild(1).GetComponent<Light2D>();
        staffRange = player.transform.GetChild(2).GetComponent<Light2D>();
        staffRange.pointLightOuterRadius = maxMagnetDistance;
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
            staffLight.color = Color.red;
            staffLight.gameObject.SetActive(true);
            TurnMagnetOn();
        }
        else if (Input.GetMouseButton(1))
        {
            playerPolarity = Polarity.positive;
            staffRenderer.sprite = blueStaff;
            staffLight.color = Color.blue;
            staffLight.gameObject.SetActive(true);
            TurnMagnetOn();
        }
        else
        {
            forceDirection = Vector2.zero;
            staffLight.gameObject.SetActive(false);
            staffPower.Play("PowerOff");
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
                if(playerPolarity == Polarity.positive)
                    staffPower.Play("BlueRepel");
                else
                    staffPower.Play("RedRepel");
                //Debug.Log("afasta");
            }
            else
            {
                forceDirection = destinationDirection.normalized;
                if (playerPolarity == Polarity.positive)
                    staffPower.Play("BlueAttract");
                else
                    staffPower.Play("RedAttract");
                //Debug.Log("atrai");
            }
            forceDirection = new Vector2(forceDirection.x * xForce, forceDirection.y * yForce).normalized;
        }
        else
        {
            forceDirection = Vector2.zero;
            staffPower.Play("PowerOff");
        }
    }



}
