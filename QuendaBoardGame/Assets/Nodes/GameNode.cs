using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        game.AllowMovement();
        if (m_gameType == GameType.food)
        {

        }
    }
}