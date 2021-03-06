﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Food object for food minigame
 * Written by Nathan Gane
 * */
public class Food : MonoBehaviour {
    public bool m_isGood;
    public string m_name;
    public int m_hungerEffect;
    public int m_scoreEffct;
    public float m_characterEffectDuration;
    public float m_characterEffectHunger;
    public bool m_characterEffectFlip;
    public float m_characterEffectSpeed;
    public float m_life;

    // Use this for initialization
    void Start ()
    {
        if (gameObject.CompareTag("Buried"))
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            color.a = 0f;
            gameObject.GetComponent<Renderer>().material.color = color;
        }
    }
	
	// Update is called once per frame
	void Update () {
        m_life -= Time.deltaTime;
        if(m_life <= 0)
        {
            Object.Destroy(gameObject);
        }
	}
}
