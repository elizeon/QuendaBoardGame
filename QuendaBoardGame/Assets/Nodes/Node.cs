using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour//System.Object
{

    Game m_game;
    public Game game { get { return m_game; }set { m_game = value; } }
    
    
    public void Start()
    {
        m_game = FindObjectOfType<Game>();
    }

    
    public enum NodeType { none,crossroads,quiz,game,merge };
    private NodeType m_type = NodeType.none;
    public NodeType type { get { return m_type; } set { m_type = value; } }

    public virtual void PerformAction()
    {
        m_game.AllowStartMovement();
        
    }
}

