using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Card class for card minigame
 * Written by Maddy Topaz
 * */
public class Card : MonoBehaviour
{

    public bool matchFound = false;

    [SerializeField]
    private bool flipped;
    [SerializeField]
    private string cardValue;
    [SerializeField]
    private bool initialized = false;

    private Sprite cardBack;
    private Sprite cardFace;

    GameObject manager;

    // Use this for initialization
    void Start ()
    {
        flipped = false;
        manager = GameObject.FindGameObjectWithTag("Manager");

        //FindObjectOfType<GameManager>().matches = 13;
	}

    public void FlipCard()
    {
        if (manager.GetComponent<GameManager>().card1 == null || manager.GetComponent<GameManager>().card2 == null)
        {
            if (!matchFound)
            {
                if (!flipped)
                {
                    GetComponent<Image>().sprite = cardFace;
                    flipped = true;
                    manager.GetComponent<GameManager>().numFlipped++;

                    if (manager.GetComponent<GameManager>().card1 == null)
                    {
                        manager.GetComponent<GameManager>().card1 = this;
                    }
                    else
                    {
                        manager.GetComponent<GameManager>().card2 = this;
                    }
                }
            }
        }           
    }

	// Update is called once per frame
	void Update ()
    {
		
	}

    public Sprite CardFace
    {
        get { return cardFace; }
        set { cardFace = value; }
    }
    public Sprite CardBack
    {
        get { return cardBack; }
        set { cardBack = value; }
    }

    public string CardValue
    {
        get { return cardValue; }
        set { cardValue = value; }
    }

    public bool Flipped
    {
        get { return flipped;  }
        set { flipped = value;  }
    }

    public bool Initialized
    {
        get { return initialized; }
        set { initialized = value; }
    }

    public void FalseCheck()
    {
        StartCoroutine(Pause());
        manager.GetComponent<GameManager>().numFlipped--;
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(.50f);

        if(flipped)
        {
            GetComponent<Image>().sprite = cardBack;
            flipped = false;      
        }

    }







        
}
