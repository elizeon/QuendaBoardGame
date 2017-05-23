using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour

{

    List<Quiz> m_quizzes = new List<Quiz>();

    public List<Quiz> quizzes { get { return m_quizzes; } }

    [SerializeField]
    public GameObject scene;
    [SerializeField]
    bool debug;

    [SerializeField]
    float m_playerSpeed = 1.0f;

    [SerializeField]
    GameObject m_player;

    [SerializeField]
    GameObject m_playerMesh;

    [SerializeField]
    Camera m_camera;

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


    public void TriggerCamera(bool active)
    {
        m_camera.gameObject.SetActive(active);
    }
    // Use this for initialization
    void Start()
    {

        m_currentNode = m_allPaths[0].transform.GetChild(0).GetComponent<Node>();
        m_currentPath = m_allPaths[0];
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

        a = new Quiz();
        a.question = "You wonder if you should go search for water. How often do Quendas need water?";
        a.quizNode1 = "Often - Quendas need to live in an area with frequent sources of fresh water.";
        a.quizNode2 = "Not often, they get most of the hydration they need from food.";
        a.correctMessage = "Correct! Quendas get most of the hydration they need from food.";
        a.incorrectMessage = "Incorrect. Quendas get most of the hydration they need from food.";
        a.hint = " ";
        a.answer = 2;
        m_quizzes.Add(a);

        a = new Quiz();
        a.question = "You want to find a nice place to stay for a while. What makes a garden or land beneficial to Quendas?";
        a.quizNode1 = "Don't offer Quendas food too often, have a large area, grow dense low vegetation, keep predators out, and monitor the Quendas to help research.";
        a.quizNode2 = "Offer them food, have a small area, grow sparse low vegetation, encourage all wildlife, and leave the Quendas alone.";
        a.quizNode3 = "Don't offer Quendas food too often, have a large area, grow lots of grass, keep predators out, and leave the Quendas alone.";
        a.quizNode4 = "Offer them food, have a large area, grow dense low vegetation, keep predators out, and monitor the Quendas to help research.";
        a.correctMessage = "Correct! Not feeding Quendas too much stops them from starving if you leave. A large area allows them to find territory. Dense low vegetation and few predators provides safety, and helping research Quendas can improve our understanding.";
        a.incorrectMessage = "Incorrect. Not feeding Quendas too much stops them from starving if you leave. A large area allows them to find territory. Dense low vegetation and few predators provides safety, and helping research Quendas can improve our understanding.";
        a.hint = "Hint: Offering Quendas food often can make them dependent on you.";
        a.answer = 1;
        m_quizzes.Add(a);

        a = new Quiz();
        a.question = "You want to decide when to start moving and searching for food today. When are Quendas most active?";
        a.quizNode1 = "Sunrise.";
        a.quizNode2 = "Midday.";
        a.quizNode3 = "Dusk.";
        a.quizNode4 = "Night.";

        a.correctMessage = "Correct! Quendas are usually most active at dusk.";
        a.incorrectMessage = "Incorrect. Quendas are usually most active at dusk.";
        a.hint = " ";
        a.answer = 3;
        m_quizzes.Add(a);

        a = new Quiz();
        a.question = "You find a quenda missing some fur and their tail. You should figure out why in case there is danger nearby. Why might a Quenda be missing their fur or tail?";
        a.quizNode1 = "They shed and regrow their fur and tail.";
        a.quizNode2 = "Males can fight for territory and mating.";
        a.correctMessage = "Correct! Males can fight for territory and mating.";
        a.incorrectMessage = "Incorrect. Males can fight for territory and mating.";
        a.hint = " ";
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
        currentPath = allPaths[index];
        SetCurrentNode(currentTileIndex);

    }

    int oldIndex = 0;
    int currentPathIndex = 0;
    /// <summary>
    /// Moves to a new path, starting on its first node.
    /// </summary>
    /// <param name="index">Path index.</param>
    public void SetCurrentPathAtStart(int index)
    {
        oldIndex = currentPathIndex;
        Debug.Log("Setting new path at start.");
        currentPath = allPaths[index];
        currentPathIndex = index;
        SetCurrentNode(0);

        if (m_tilesToMove > 0)
        {
            //if (lastNode.type == Node.NodeType.none)
            //{
            if(m_tilesToMove>currentPath.transform.childCount)
            {
                Debug.Log("Capping movement to new path's end.");
                m_tilesToMove = currentPath.transform.childCount;

            }
            m_movingOnPath = true;
        }
    }

    /// <summary>
    /// Moves to a new path, starting on its last node.
    /// </summary>
    /// <param name="index">Path index.</param>
    public void SetCurrentPathAtEnd(int index)
    {
        oldIndex = currentPathIndex;
        Debug.Log("Setting new path at end.");
        currentPath = allPaths[index];
        currentPathIndex = index;
        SetCurrentNode(currentPath.transform.childCount - 1);

        if (m_tilesToMove < 0)
        {
            //if (lastNode.type == Node.NodeType.none)
            //{
            m_tilesToMove = -(currentPath.transform.childCount - (m_tilesToMove + 1));
            //}
            m_movingOnPath = true;
        }
    }



    /// <summary>
    /// Moves to node of given index on the current path.
    /// </summary>
    /// <param name="index">Node index.</param>
    public void SetCurrentNode(int index)
    {
        currentNode = currentPath.transform.GetChild(index).GetComponent<Node>();
        m_currentTileIndex = index;

    }



    [SerializeField]
    private List<GameObject> m_allPaths = new List<GameObject>();
    //private List<ListWrapper> m_allPaths = new List<ListWrapper>();
    /// <summary>
    /// A list of all paths in the game.
    /// </summary>
    public List<GameObject> allPaths { get { return m_allPaths; } set { m_allPaths = value; } }

    private GameObject m_currentPath;
    /// <summary>
    /// The current path the player is on.
    /// </summary>
    public GameObject currentPath
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="oldSceneBuildIndex"></param>
    /// <param name="newSceneBuildIndex"></param>
    /// <returns></returns>
    IEnumerator LoadSceneCoroutine(int oldSceneBuildIndex, int newSceneBuildIndex, bool additive)
    {

        bool complete = false;
        while (complete == false)
        {
            Scene nextScene = SceneManager.GetSceneByBuildIndex(newSceneBuildIndex);
            yield return new WaitForEndOfFrame();

            //SceneManager.UnloadSceneAsync(oldSceneBuildIndex);//SceneManager.GetActiveScene().buildIndex);

            //yield return new WaitForEndOfFrame();
            if (additive)
            {
                SceneManager.LoadScene(newSceneBuildIndex, LoadSceneMode.Additive);

            }
            else
            {
                SceneManager.LoadScene(newSceneBuildIndex, LoadSceneMode.Single);

            }

            yield return new WaitForEndOfFrame();



            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(newSceneBuildIndex));
            complete = true;
        }
        yield return null;

    }


    public void LoadScene(int oldSceneBuildIndex, int newSceneBuildIndex, bool additive)
    {

        if (additive)
        {

            StartCoroutine(LoadSceneCoroutine(oldSceneBuildIndex, newSceneBuildIndex, additive));

        }
        else
        {
            StartCoroutine(LoadSceneCoroutine(oldSceneBuildIndex, newSceneBuildIndex, additive));

        }

    }

    void SetPlayerAnimation(bool state)
    {
        if(player.GetComponentInChildren<Animator>().enabled != state)
        {
            player.GetComponentInChildren<Animator>().enabled = state;
        }
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
                        if (!messageBox.Active())
                        {

                            m_movedTiles = 0;
                            m_movingOnPath = false;
                            m_currentNode.PerformAction();
                        }
                        else
                        {
                            // Stops message box from resuming play, as there is a tile action in queue that
                            // will not allow the player to move until it is complete.
                            if(messageBox.canMoveOnResume)
                            {
                                messageBox.canMoveOnResume = false;
                            }
                        }
                    }

                }
                else
                {
                    SetPlayerAnimation(false);

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
        else
        {
            SetPlayerAnimation(false);

        }

    }

    bool m_paused = false;
    public bool paused { get; set; }

    int m_tilesToMove;
    public int tilesToMove { get { return m_tilesToMove; } }
    int m_movedTiles = 0;

    void GoToNextNode()
    {
        SetPlayerAnimation(true);
        m_currentTileIndex += 1;
        m_currentNode = m_currentPath.transform.GetChild(m_currentTileIndex).GetComponent<Node>();
        m_currentNode.PerformAction();
        m_playerMesh.transform.LookAt(m_currentNode.transform, new Vector3(0, 1, 0));

    }

    void GoToPrevNode()
    {
        SetPlayerAnimation(true);

        m_currentTileIndex -= 1;
        m_currentNode = m_currentPath.transform.GetChild(m_currentTileIndex).GetComponent<Node>();
        m_currentNode.PerformAction();
        m_playerMesh.transform.LookAt(m_currentNode.transform, new Vector3(0, 1, 0));

    }

    void GoToNextNodeWithoutEvent()
    {
        SetPlayerAnimation(true);

        if (m_currentNode.type != Node.NodeType.merge)
        {
            m_currentTileIndex += 1;
        }
        m_currentNode = m_currentPath.transform.GetChild(m_currentTileIndex).GetComponent<Node>();
        m_playerMesh.transform.LookAt(m_currentNode.transform, new Vector3(0, 1, 0));


    }

    void GoToPrevNodeWithoutEvent()
    {
        SetPlayerAnimation(true);

        if (m_currentNode.type != Node.NodeType.merge && m_currentTileIndex > 0)
        {
            m_currentTileIndex -= 1;
        }
        m_currentNode = m_currentPath.transform.GetChild(m_currentTileIndex).GetComponent<Node>();
        m_playerMesh.transform.LookAt(m_currentNode.transform, new Vector3(0, 1, 0));


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

            /*
            Node lastNode = currentPath[currentPath.Count - 1].GetComponent<Node>();
            if (lastNode.type == Node.NodeType.none)
            {
                m_tilesToMove = (currentPath.Count - (currentTileIndex + 1));
            }
            */
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