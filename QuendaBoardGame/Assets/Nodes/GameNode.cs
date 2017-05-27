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
        game.scene.SetActive(false);
        game.TriggerCamera(false);
        game.DisallowStartMovement();
        game.LoadScene(0, 1, true);
        
    }

    void LoadFoodGame()
    {
        game.scene.SetActive(false);

        game.TriggerCamera(false);
        game.DisallowStartMovement();
        game.LoadScene(0, 5, true);
    }

    void LoadCardGame()
    {
        game.scene.SetActive(false);

        game.TriggerCamera(false);
        game.DisallowStartMovement();
        game.LoadScene(0, 6, true);
    }
}