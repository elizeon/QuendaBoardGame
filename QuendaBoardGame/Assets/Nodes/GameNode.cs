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

    public enum GameType { food, cat, road };
    public GameType m_gameType;
    public override void PerformAction()
    {
        game.AllowStartMovement();
        if (m_gameType == GameType.food)
        {

        }
        if (m_gameType == GameType.cat)
        {
            Debug.Log("Loading cat game!");
            LoadCatGame();
        }
    }
    
    void LoadCatGame()
    {
        game.scene.SetActive(false);
        game.TriggerCamera(false);
        game.DisallowStartMovement();
        game.LoadScene(0, 1, true);
        
    }
}