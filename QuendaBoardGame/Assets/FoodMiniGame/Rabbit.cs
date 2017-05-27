using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Rabbit competitor in Food Minigame
 * By Nathan Gane
 * 
 * */
public class Rabbit : MonoBehaviour {
    // Use this for initialization
    public float speed;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
        Vector3 target = new Vector3(0,0,0);
        float minimumDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        for(int i = 0; i < foods.Length; i++)
        {
            float distance = Vector3.Distance(foods[i].transform.position, currentPos);
            if (distance < minimumDistance)
            {
                target = foods[i].transform.position;
                minimumDistance = distance;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Object.Destroy(collision.gameObject);
        }
    }
}
