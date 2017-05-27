using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Quenda vision (transparent food that appears as you go closer) for food minigame
 * By Nathan Gane
 * 
 * */
public class QuendaVision : MonoBehaviour
{
    public float m_radius;

    // Use this for initialization
    void Start()
    {
        GetComponent<CircleCollider2D>().radius = m_radius;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Player = GameObject.Find("Quenda");
        transform.position = Player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buried"))
        {
            Color color = collision.gameObject.GetComponent<Renderer>().material.color;
            color.a = 1 - (Vector3.Distance(collision.transform.position, transform.position) / m_radius);
            collision.gameObject.GetComponent<Renderer>().material.color = color;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buried"))
        {
            Color color = collision.gameObject.GetComponent<Renderer>().material.color;
            color.a = 1 - (Vector3.Distance(collision.transform.position, transform.position) / m_radius);
            collision.gameObject.GetComponent<Renderer>().material.color = color;
        }
    }
}
