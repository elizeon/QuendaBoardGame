﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float speedMultiplier { get; set; }
    public bool flipModifier { get; set; }
    public float hungerGenerationModifier { get; set; }
    public float duration { get; set; }
}

public class Quenda : MonoBehaviour {
    private float hunger;
    private List<Effect> effects;
    private bool digCompleted;
    public float speed;
    public string digKey;
    public string quendaVisionKey;

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

        if (Input.GetButtonDown(digKey))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Effect n = new Effect();
            n.speedMultiplier = collision.GetComponent<Food>().m_characterEffectSpeed;
            n.flipModifier = collision.GetComponent<Food>().m_characterEffectFlip;
            n.hungerGenerationModifier = collision.GetComponent<Food>().m_characterEffectHunger;
            n.duration = collision.GetComponent<Food>().m_characterEffectDuration;
            Object.Destroy(collision.gameObject);
            effects.Add(n);
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
