using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour//System.Object
{

    Game m_game;
    public Game game { get { return m_game; }set { m_game = value; } }

    [SerializeField]
    bool m_endNode = false;
    /// <summary>
    /// Whether the node is the final node of the level.
    /// </summary>
    public bool endNode { get { return m_endNode; } set { m_endNode = value; } }

    public void Start()
    {
        m_game = FindObjectOfType<Game>();
    }

    
    public enum NodeType { none,crossroads,quiz,game,merge };
    private NodeType m_type = NodeType.none;
    public NodeType type { get { return m_type; } set { m_type = value; } }

    public virtual void PerformAction()
    {
        m_game.AllowMovement();

        if (m_endNode)
        {
            Debug.Log("You completed the level!");
        }
    }
}

