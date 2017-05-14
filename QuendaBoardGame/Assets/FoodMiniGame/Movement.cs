using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;
    public bool dirFlip;
    private Vector3 target;
    private int controlMethod = 0;
    //0 is all methods active
    //1 is mouse
    //2 is WASD
    //3 is arrow keys


	// Use this for initialization
	void Start () {
        target = transform.position;
        controlMethod = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(controlMethod == 0)
        {
            MouseMovement();
        }else
        {
            if (controlMethod == 1)
            {
                MouseMovement();
            }
            else
            {
                if (controlMethod == 2)
                {

                }
                else
                {
                    if (controlMethod == 3)
                    {

                    }
                }
            }
        }

        if(target != transform.position)
        {
            Vector3 angle = transform.position - target;
            float ang = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
            ang += 90;
            //transform.LookAt(target, Vector3.forward);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, ang));
            if (dirFlip)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, -speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
    }

    void MouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 30;
            target = Camera.main.ScreenToWorldPoint(mousePos);
            target.z = transform.position.z;
            Debug.Log(target);
        }else
        { 
            target = transform.position;
        }
    }
}
