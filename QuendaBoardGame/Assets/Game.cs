using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour

{

    List<Quiz> m_quizzes = new List<Quiz>();

    public List<Quiz> quizzes { get { return m_quizzes; } }

    [SerializeField]
    bool debug;

    [SerializeField]
    float m_playerSpeed = 1.0f;

    [SerializeField]
    GameObject m_player;

    public GameObject player { get { return m_player; } }

    [SerializeField]
    GameObject m_quizScreen;
    public GameObject quizScreen { get { return m_quizScreen; } }

    [SerializeField]
    GameObject m_messageBox;
    public MessageBox messageBox { get { return m_messageBox.GetComponent<MessageBox>(); } }

    [SerializeField]
    GameObject m_moveButton;

    [SerializeField]
    GameObject m_crossroadsMsgBox;
    public GameObject crossroadsMsgBox { get { return m_crossroadsMsgBox; } }


    // Use this for initialization
    void Start()
    {

        m_currentNode = m_allPaths[0].list[0].GetComponent<Node>();
        m_currentPath = m_allPaths[0].list;
        m_currentTileIndex = 0;
        Debug.Log("Test controls - keys 1-6: move 1 to 6 tiles. B: move random number between 1-6");
        m_canMove = true;
        m_messageBox.SetActive(true);
        m_quizScreen.SetActive(true);
        m_crossroadsMsgBox.SetActive(false);

        Quiz a;
        a = new Quiz();
        a.question = "How do Quendas help the environment?";
        a.quizNode1 = "Control introduced insect pests.";
        a.quizNode2 = "Benefit plant growth.";
        a.quizNode3 = "Quendas don't help the environment.";
        a.correctMessage = "Correct! Quenda poo helps plants grow.";
        a.incorrectMessage = "Incorrect. Quendas benefit plant growth.";
        a.hint = "Hint: Quendas eat (and poop) underground insects, fungi and plant matter.";
        a.answer = 2;
        m_quizzes.Add(a);
    }

    /// <summary>
    /// The listwrapper is as a workaround to insert values to lists of lists using the unity editor - there is no other way.
    /// </summary>
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

        if (m_tilesToMove > 0)
        {
            Node lastNode = currentPath[currentPath.Count - 1].GetComponent<Node>();
            if (lastNode.type == Node.NodeType.none)
            {
                m_tilesToMove = (currentPath.Count - (currentTileIndex + 1));
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
    public int currentTileIndex { get { return m_currentTileIndex; } set { m_currentTileIndex = value; } }


    private Node m_currentNode;
    /// <summary>
    /// The current node the player is on.
    /// </summary>
    public Node currentNode { get { return m_currentNode; } set { m_currentNode = value; } }

    public bool HasTilesToMove()
    {
        if (m_tilesToMove != 0)
        {
            return true;
        }
        return false;
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
        if(!m_paused)
        {
            if (m_player.transform.position != m_currentNode.transform.position)
            {
                MoveToNode(m_currentNode);
            }
            else
            {
                if (m_movingOnPath)
                {
                    if (m_tilesToMove != 0)
                    {
                        if (m_tilesToMove < 0)
                        {
                            GoToPrevNodeWithoutEvent();

                        }
                        else
                        {
                            GoToNextNodeWithoutEvent();

                        }
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
            if (debug)
            {

            }
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

    }

    bool m_paused = false;
    public bool paused { get; set; }

    int m_tilesToMove;
    int m_movedTiles = 0;

    void GoToNextNode()
    {
        m_currentTileIndex += 1;
        m_currentNode = m_currentPath[m_currentTileIndex].GetComponent<Node>();
        m_currentNode.PerformAction();
    }

    void GoToPrevNode()
    {
        m_currentTileIndex -= 1;
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

    void GoToPrevNodeWithoutEvent()
    {
        if (m_currentNode.type != Node.NodeType.merge && m_currentTileIndex > 0)
        {
            m_currentTileIndex -= 1;
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

    /// <summary>
    /// Moves the on path.
    /// </summary>
    /// <param name="count">The count.</param>
    public void MoveOnPath(int count)
    {
        if (m_canMove)
        {
            m_tilesToMove = count;
            m_movingOnPath = true;

            Node lastNode = currentPath[currentPath.Count - 1].GetComponent<Node>();
            if (lastNode.type == Node.NodeType.none)
            {

                m_tilesToMove = (currentPath.Count - (currentTileIndex + 1));
            }

            DisallowStartMovement();
        }


    }

    /// <summary>
    /// Moves the player with button.
    /// Rolls random number between 1 and 6.
    /// </summary>
    /// <param name="diceOutput">The dice output. Must have Text component.</param>
    public void MoveWithButton(GameObject diceOutput)
    {
        int random = Random.Range(1, 6);
        diceOutput.GetComponent<Text>().text = random.ToString();
        MoveOnPath(random);

    }
    /// <summary>
    /// Gives the player ability to roll the dice and move once.
    /// </summary>
    public void AllowStartMovement()
    {
        m_canMove = true;
        m_moveButton.SetActive(true);
    }

    /// <summary>
    /// Removes the player ability to roll the dice and move.
    /// </summary>
    public void DisallowStartMovement()
    {
        m_canMove = false;
        m_moveButton.SetActive(false);
    }

    bool m_canMove = false;

    public void CloseCrossroadsMsgBox()
    {
        m_crossroadsMsgBox.SetActive(false);
    }

    /// <summary>
    /// Goes left at crossroads. (For UI button functionality)
    /// Must be on a crossroads node.
    /// </summary>
    public void GoLeftAtCrossroads()
    {
        CrossroadsNode node = currentNode.GetComponent<CrossroadsNode>();
        node.GoLeft();
    }

    /// <summary>
    /// Goes right at crossroads. (For UI button functionality)
    /// Must be on a crossroads node.
    /// </summary>
    public void GoRightAtCrossroads()
    {
        CrossroadsNode node = currentNode.GetComponent<CrossroadsNode>();
        node.GoRight();
    }

    public void StopMovement()
    {
        m_tilesToMove = 0;
        m_movingOnPath = false;
    }
    
}