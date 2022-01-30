using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    private Vector3 mousePos;

    private GameObject staffPower;
    // Update is called once per frame
    private void Start()
    {
        staffPower = aimTransform.GetChild(1).gameObject;
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle;
        float powerAngle;


        aimTransform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = aimDir.x < 0;

        if (aimDir.x < 0)
        {
            angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg + 90;
            powerAngle = angle - 45;

        }
        else
        {
            angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            powerAngle = angle + 45;
        }

        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        staffPower.transform.eulerAngles = new Vector3(0, 0, powerAngle);
        
    }
}
