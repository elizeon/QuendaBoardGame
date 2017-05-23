using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Todo make member variables private for all these classes.
/// </summary>
[System.Serializable]
public class GameNode : Node
{
    new void Start()
    {
        base.Start();
        type = NodeType.game;
    }

    public enum GameType { food, cat, card };
    public GameType m_gameType;
    public override void PerformAction()
    {
        game.AllowStartMovement();
        if (m_gameType == GameType.food)
        {
            Debug.Log("Loading food game!");
            LoadFoodGame();
        }
        if (m_gameType == GameType.cat)
        {
            Debug.Log("Loading cat game!");
            LoadCatGame();
        }
        if (m_gameType == GameType.card)
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
        game.TriggerCamera(false);
        game.DisallowStartMovement();
        game.LoadScene(0, 5, true);
    }

    void LoadCardGame()
    {
        game.TriggerCamera(false);
        game.DisallowStartMovement();
        game.LoadScene(0, 6, true);
    }
}