using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Board game node for starting minigames
 * Written by Elizabeth Haynes
 *
 **/
[System.Serializable]
public class GameNode : Node
{
    new void Start()
    {
        base.Start();
        type = NodeType.game;
    }

    public enum GameType { food, cat, card };
    public GameType gameType;
    public override void PerformAction()
    {
        game.AllowStartMovement();
        if (gameType == GameType.food)
        {
            Debug.Log("Loading food game!");
            LoadFoodGame();
        }
        if (gameType == GameType.cat)
        {
            Debug.Log("Loading cat game!");
            LoadCatGame();
        }
        if (gameType == GameType.card)
        {
            Debug.Log("Loading card game!");
            LoadCardGame();
        }
    }
    
    void LoadCatGame()
    {
        string howToPlay = "You're spotted by a cat! You quickly hide. You must find out how to get past, to safety. Reach the right side of the screen to win. Click on the screen to move to that position. Hide in bushes to hide from the cat. ";
        game.messageBox.gameObject.SetActive(true);
        game.messageBox.DisplayMessageBox(howToPlay);
        game.ReadyMoveToLevel(11);
    }

    void LoadFoodGame()
    {
        string howToPlay = "You find a good area to search for food. Collect the food that is good for quendas and avoid dangerous foods. Good food for Quendas is usually underground. They will become more visible as you sniff them out by getting closer to them. Click the screen to move there. Fill your hunger bar by eating good foods. Bad foods will lower your hunger bar.";
        game.messageBox.gameObject.SetActive(true);
        game.messageBox.DisplayMessageBox(howToPlay);
        game.ReadyMoveToLevel(12);

    }

    void LoadCardGame()
    {
        string howToPlay = "You've seen something dangerous, but you're not sure. You must remember what things are good and bad for Quendas. Match all of the cards with their answer to solve the problem. Eg. Match DIET with an image of a Quenda's ideal diet. Click two cards to check if they match. ";
        game.messageBox.gameObject.SetActive(true);
        game.messageBox.DisplayMessageBox(howToPlay);
        game.ReadyMoveToLevel(13);

    }
}
