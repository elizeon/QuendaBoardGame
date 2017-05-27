using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Food effects for food minigame
 * By Nathan Gane
 * */
public class Effect : MonoBehaviour
{
    public float speedMultiplier { get; set; }
    public bool flipModifier { get; set; }
    public float hungerGenerationModifier { get; set; }
    public float duration { get; set; }
}

/**
 * 
 * Quenda player class for food minigame
 * By Nathan Gane
 * */

public class Quenda : MonoBehaviour {
    public float hunger;
    private List<Effect> effects;
    private bool digCompleted;
    public float speed;

	// Use this for initialization
	void Start () {
        hunger = 20;
        effects = new List<Effect>();
    }
	
	// Update is called once per frame
	void Update () {
        float speedModifier = 1;
        bool flipModifier = false;
        float hungerMod = 1;

        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].duration -= Time.deltaTime;
            if (effects[i].duration <= 0)
            {
                effects.RemoveAt(i);
            }else
            {
                hungerMod += effects[i].hungerGenerationModifier;
                speedModifier += effects[i].speedMultiplier;
                if (!flipModifier)
                {
                    flipModifier = effects[i].flipModifier;
                }            
            }
        }
        hunger -= (Time.deltaTime * hungerMod);
        GetComponent<Movement>().speed = speed * speedModifier;
        GetComponent<Movement>().dirFlip = flipModifier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food") || collision.gameObject.CompareTag("Buried"))
        {
            Effect n = new Effect();
            n.speedMultiplier = collision.GetComponent<Food>().m_characterEffectSpeed;
            n.flipModifier = collision.GetComponent<Food>().m_characterEffectFlip;
            n.hungerGenerationModifier = collision.GetComponent<Food>().m_characterEffectHunger;
            n.duration = collision.GetComponent<Food>().m_characterEffectDuration;
            hunger += collision.GetComponent<Food>().m_hungerEffect;
            Object.Destroy(collision.gameObject);
            effects.Add(n);
        }
    }

    private void Dig()
    {

    }
}
