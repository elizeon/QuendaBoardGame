using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour

{
    [SerializeField]
    float m_playerSpeed = 1.0f;

    [SerializeField]
    GameObject m_player;

    [SerializeField]
    GameObject m_quizScreen;

    public GameObject quizScreen { get { return m_quizScreen; } }

    [System.Serializable]
    public class ListWrapper
    {
        public List<GameObject> list;
    }

    /// <summary>
    /// Moves to a new path but does not reset node index.
    /// You will move to the equivalent node on the new path.
    /// </summary>
    /// <param name="index">Path index.</param>
    private void SetCurrentPath(int index)
    {
        currentPath = allPaths[index].list;
        SetCurrentNode(currentTileIndex);
       
    }
    /// <summary>
    /// Moves to a new path, starting on its first node.
    /// </summary>
    /// <param name="index">Path index.</param>
    public void SetCurrentPathAtStart(int index)
    {
        Debug.Log("Setting new path.");
        currentPath = allPaths[index].list;
        SetCurrentNode(0);

        if(m_tilesToMove > 0)
        {
            Node lastNode = currentPath[currentPath.Count - 1].GetComponent<Node>();
            if (lastNode.type == Node.NodeType.none)
            {
                m_tilesToMove = (currentPath.Count - (currentTileIndex+1));
            }
            m_movingOnPath = true;
        }
    }
    /// <summary>
    /// Moves to node of given index on the current path.
    /// </summary>
    /// <param name="index">Node index.</param>
    public void SetCurrentNode(int index)
    {
        currentNode = currentPath[index].GetComponent<Node>();
        m_currentTileIndex = index;

    }



    [SerializeField]
    private List<ListWrapper> m_allPaths = new List<ListWrapper>();
    /// <summary>
    /// A list of all paths in the game.
    /// </summary>
    public List<ListWrapper> allPaths { get { return m_allPaths; } set { m_allPaths = value; } }

    private List<GameObject> m_currentPath = new List<GameObject>();
    /// <summary>
    /// The current path the player is on.
    /// </summary>
    public List<GameObject> currentPath
    {
        get { return m_currentPath; }
        set { m_currentPath = value; }
    }

    private int m_currentTileIndex;
    /// <summary>
    /// The index of the node the player is on in the current path.
    /// </summary>
    public int currentTileIndex { get { return m_currentTileIndex; }set { m_currentTileIndex = value; } }


    private Node m_currentNode;
    /// <summary>
    /// The current node the player is on.
    /// </summary>
    public Node currentNode { get { return m_currentNode; }set { m_currentNode = value; } }

    // Use this for initialization
    void Start()
    {

        m_currentNode = m_allPaths[0].list[0].GetComponent<Node>();
        m_currentPath = m_allPaths[0].list;
        m_currentTileIndex = 0;
        Debug.Log("Test controls - keys 1-6: move 1 to 6 tiles. B: move random number between 1-6");
        m_canMove = true;

    }

    // Update is called once per frame
    void Update()
    {

        // Player movement
        /*
        if (!(m_player.transform.position == m_currentNode.transform.position))
        {
            MoveToNode(m_currentNode);
        }
        */

        // BUG: Doesn't go if tiles to move more than count
        // Instead just make it set tiles to move to count when you set this.

        if (m_player.transform.position != m_currentNode.transform.position)
        {
            MoveToNode(m_currentNode);
        }
        else
        {
            if (m_movingOnPath)
            {
                if (m_tilesToMove!= 0)
                {
                    GoToNextNodeWithoutEvent();
                    if (m_currentNode.type == Node.NodeType.crossroads)
                    {
                        m_movedTiles = 0;
                        m_movingOnPath = false;
                        m_currentNode.PerformAction();
                    }
                    else
                    {
                        if (m_currentNode.type == Node.NodeType.merge)
                        {
                            m_movedTiles = 0;
                            m_movingOnPath = false;
                            m_currentNode.PerformAction();
                        }
                    }

                    
                    //SetCurrentNode(m_currentTileIndex+1)
                    m_movedTiles += 1;
                    m_tilesToMove -= 1;
                }
                else
                {
                    m_movedTiles = 0;
                    m_movingOnPath = false;
                    m_currentNode.PerformAction();
                }

            }
        }
        
        /*
        if (Input.GetKeyDown(KeyCode.N))
        {
            GoToNextNode();
        }
        */
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            int randNumber = Random.Range(1, 6);
            Debug.Log(randNumber);
            MoveOnPath(randNumber);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            MoveOnPath(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            MoveOnPath(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            MoveOnPath(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            MoveOnPath(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

            MoveOnPath(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

            MoveOnPath(6);
        }


    }

    int m_tilesToMove;
    int m_movedTiles = 0;

    void GoToNextNode()
    {
        m_currentTileIndex += 1;
        m_currentNode = m_currentPath[m_currentTileIndex].GetComponent<Node>();
        m_currentNode.PerformAction();
    }

    void GoToNextNodeWithoutEvent()
    {
        if (m_currentNode.type != Node.NodeType.merge)
        {
            m_currentTileIndex += 1;
        }
        m_currentNode = m_currentPath[m_currentTileIndex].GetComponent<Node>();
        
        
    }

    bool m_movingOnPath = false;

    /// <summary>
    /// Moves an object towards another at a given speed.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="node"></param>
    void MoveTo(GameObject obj, GameObject node, float speed)
    {
        float step = speed * Time.deltaTime;
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, node.transform.position, step);
    }

    /// <summary>
    /// Moves player towards the given node at the player's normal movement speed.
    /// </summary>
    /// <param name="node"></param>
    void MoveToNode(Node node)
    {
        MoveTo(m_player, node.gameObject, m_playerSpeed);
       
    }

    void MoveOnPath(int count)
    {
        if(m_canMove)
        {
            m_tilesToMove = count;
            m_movingOnPath = true;

            Node lastNode = currentPath[currentPath.Count - 1].GetComponent<Node>();
            if (lastNode.type == Node.NodeType.none)
            {

                m_tilesToMove = (currentPath.Count - (currentTileIndex + 1));
            }

            m_canMove = false;
        }
        
        
    }
    /// <summary>
    /// Gives the player ability to roll the dice and move once.
    /// </summary>
    public void AllowMovement()
    {
        m_canMove = true;
    }
    bool m_canMove = false;
}
