using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float speedMultiplier { get; set; }
    public float movementAngleModifier { get; set; }
    public float movementSwayModifier { get; set; }
    public float hungerGenerationModifier { get; set; }
    public float duration { get; set; }
}

public class Quenda : MonoBehaviour {
    private float hunger;
    private List<Effect> effects;
    private bool digCompleted;
    public string digKey;
    public string quendaVisionKey;

	// Use this for initialization
	void Start () {
        hunger = 20;
    }
	
	// Update is called once per frame
	void Update () {
        hunger -= Time.deltaTime;
       
        for(int i = 0; i < effects.Count; i++)
        {
            if(effects[i].duration == 0)
            {
                effects.RemoveAt(i);
            }

        }

       
        if(Input.GetButtonDown(digKey))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
           Object.Destroy(collision.gameObject);
        }

        if (!(collision.gameObject.CompareTag("Buried")))
        {
            if (Input.GetButtonDown(digKey))
            {
                Object.Destroy(collision);
            }
        }
    }

    private void Dig()
    {

    }
}
