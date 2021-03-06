using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject playerBody;
    [SerializeField] private GameObject playerHead;

    private SpriteRenderer bodySprite;
    private SpriteRenderer headSprite;


    [Header("Death Animation")]
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private float deathPump = 5f;
    [SerializeField] private float torqueSpeed = 4f;

    GameObject headObj;
    GameObject bodyObj;

    void Start()
    {

        bodySprite = playerBody.GetComponent<SpriteRenderer>();
        headSprite = playerHead.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Persistent.current.fadeOn && !Persistent.current.paused)
        {
            FlipSprites();
        }
    }

    void FlipSprites()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDir = (mousePos - transform.position).normalized;

        headSprite.flipX = aimDir.x < 0;
        bodySprite.flipX = Input.GetAxisRaw("Horizontal") < 0;
    }

    public void DeathAnimation()
    {
        headObj = Instantiate(headPrefab, playerHead.transform.position, Quaternion.identity);
        Rigidbody2D headRb = headObj.GetComponent<Rigidbody2D>();

        headRb.velocity = Vector2.up * deathPump;
        headRb.AddTorque(torqueSpeed, ForceMode2D.Force);

        bodyObj = Instantiate(bodyPrefab, playerBody.transform.position, Quaternion.identity);
        bodyObj.GetComponent<Animator>().Play("DeathAnimation");

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Light2D>().enabled = false;
        gameObject.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled=false;
        
    }

    public void DestroyDeathRemains()
    {
        Destroy(headObj);
        Destroy(bodyObj);
    }

    public void DoSqueeze(float _xSqueeze, float _ySqueeze, float _seconds)
    {
        StartCoroutine(JumpSqueeze(_xSqueeze, _ySqueeze, _seconds));
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
}
