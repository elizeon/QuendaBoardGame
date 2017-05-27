﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * Class for in-game menu display
 * 
 * Written by Elizabeth Haynes
 * 
 * */
public class Menu : MonoBehaviour
{

    [SerializeField]
    public GameObject menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ToggleMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }
}
