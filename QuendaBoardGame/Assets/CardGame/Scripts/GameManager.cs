using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Card card1 = null;
    public Card card2 = null;

    public int numFlipped = 0;

    public List<string> categories = new List<string>();

    public List<Sprite> diet = new List<Sprite>();
    public List<Sprite> habitat = new List<Sprite>();
    public List<Sprite> predator = new List<Sprite>();
    public List<Sprite> threat = new List<Sprite>();
    public GameObject CardGame;
    Game m_game;
    bool m_result;
    public Sprite cardBack;

    public List<Card> cards = new List<Card>();

    public Text matchText;


    public int matches { get; set; }

    public Dictionary<string, Sprite> cardLookup = new Dictionary<string, Sprite>();

    public Dictionary<string, List<Sprite>> matchingImage = new Dictionary<string, List<Sprite>>();

    public void InitializeCategories()
    {
        categories.Add("Diet");
        categories.Add("Habitat");
        categories.Add("Predator");
        categories.Add("Threat");
    }

    public void InitializeMatches()
    {
        matchingImage.Add("Diet", diet);
        matchingImage.Add("Habitat", habitat);
        matchingImage.Add("Predator", predator);
        matchingImage.Add("Threat", threat);
    }

    public void InitializeCards()
    {
        List<int> freeSpots = new List<int>();

        for (int i = 0; i < cards.Count; i++)
        {
            freeSpots.Add(i);
        }

        for(int i=0; i < cards.Count; i++)
        {
            //Intializes two cards at a time until all cards initalized
            if(!cards[i].Initialized)
            {
                //Generate a category for this card
                string value = categories[Random.Range(0, 4)];

                //Give the card that category
                cards[i].CardValue = value;
                //Set the cards image
                SetCardImage(i, value);                
                //Index i no longer a free spot to put a card match
                freeSpots.Remove(i);
                //Card is now initialized
                cards[i].Initialized = true;

                //Now create its match
                int index = Random.Range(0, freeSpots.Count);
                int freeSpotIndex = freeSpots[index];
                cards[freeSpotIndex].CardValue = value;
                SetCardImage(freeSpotIndex, value);
                freeSpots.Remove(freeSpotIndex);
                cards[freeSpotIndex].Initialized = true;
            }   
        }
    }

    void SetCardImage(int index, string category)
    {
        List<Sprite> possibleFaces;
        //Get the list of images I can use for this category
        possibleFaces = matchingImage[category];
        int spriteIndex = Random.Range(0, possibleFaces.Count);
        //Set the cards image
        cards[index].CardFace = possibleFaces[spriteIndex];
        cards[index].CardBack = cardBack;
    }

	// Use this for initialization
	void Start ()
    {
        InitializeCategories();
        InitializeMatches();
        InitializeCards();
        m_game = FindObjectOfType<Game>();
    }

    public void ReturnToGame(int currentScene)
    {
        Destroy(CardGame);

        m_game.scene.SetActive(true);

        m_game.TriggerCamera(true);
        m_game.AllowStartMovement();

        if (m_result)
        {
            m_game.messageBox.DisplayMessageBox("Success! Move forward 3 spaces.", true);
            m_game.MoveOnPath(3);
            m_game.AddResult("Card Game", m_result);
        }
        else
        {
            m_game.messageBox.DisplayMessageBox("You failed. Move backwards 3 spaces.", true);
            m_game.MoveOnPath(-3);
            m_game.AddResult("Card Game", m_result);
        }
    }

    public void Lose()
    {
        m_result = false;
        ReturnToGame(6);
    }

    public void Win()
    {
        m_result = true;
        ReturnToGame(6);
    }

    // Update is called once per frame
    void Update()
    {
        if (card1 != null)
        {
            if (!card1.Flipped)
            {
                card1 = null;
            }
        }
        if (card2 != null)
        {
            if (!card2.Flipped)
            {
                card2 = null;
            }

        }

        if (card1 != null && card2 != null)
        {
            if (Match())
            {
                card1.matchFound = true;
                card2.matchFound = true;

                card1 = null;
                card2 = null;
            }
            else
            {
                card1.FalseCheck();
                card2.FalseCheck();
            }
        }
    }

    public Sprite GetCardBack()
    {
        return cardBack;
    }


    bool Match()
    {
        if(card1.CardValue == card2.CardValue)
        {
            matches++;
            matchText.text = "Matches: " + matches + "/ 14";
            if(matches >= 14)
            {
                Win();
            }
            return true;
        }       
        return false;
    }

}
