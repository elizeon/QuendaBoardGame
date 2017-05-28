using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
//using UnityEditor;

/**
 * Main game class for Quenda board game
 * Written by Elizabeth Haynes
 * 
 * */
public class Game : MonoBehaviour

{

    List<Quiz> m_quizzes = new List<Quiz>();

    public List<Quiz> quizzes { get { return m_quizzes; } }
    private List<PlayerData> m_playerResults = new List<PlayerData>();

    [SerializeField]
    public GameObject scene;
    [SerializeField]
    bool debug;

    [SerializeField]
    GameObject m_overlayUI;

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

    [SerializeField]
    private LineGraph m_resultsMenu;

    [SerializeField]
    private GameObject m_creditsMenu;


    public void ShowCreditsMenu()
    {
        m_overlayUI.SetActive(false);
        Debug.Log("Showing credits.");
        m_mainMenu.SetActive(false);
        m_creditsMenu.SetActive(true);
    }

    public void ShowResultsHelpMenu()
    {

        m_mainMenu.SetActive(false);
        m_creditsMenu.SetActive(false);
        m_resultsMenu.gameObject.SetActive(false);
        m_resultsHelpMenu.SetActive(true);
    }
    [SerializeField]
    GameObject m_resultsHelpMenu;

    public void ShowResultsMenu()
    {


        m_mainMenu.SetActive(false);
        m_resultsMenu.gameObject.SetActive(true);

        Debug.Log("Showing results.");
        // for each result with n values

        m_resultsHelpMenu.SetActive(false);
        m_resultsMenu.values = new List<float>();

        List<float> newVals = new List<float>();

        int maxCount = 0;
        for (int a = 0; a < m_playerResults.Count; a++)
        {
            if(m_playerResults[a].GetCount()>maxCount)
            {
                maxCount = m_playerResults[a].GetCount();
            }
        }

        Debug.Log("Max count is " + maxCount);

        for (int i = 0; i < maxCount; i++)
        {
            newVals.Add(-1);
            for (int a = 0; a < m_playerResults.Count; a++)
            {
                PlayerData thisData = m_playerResults[a];
                if (thisData.GetCount() > i)
                {
                    newVals[i] = 0;
                    float toFloat;
                    if(thisData.m_results[i]==true)
                    {
                        toFloat = 1f;
                    }
                    else
                    {
                        toFloat = 0f;
                    }


                    newVals[i] += toFloat;
                }
            }
        }

        List<float> finalVals = new List<float>();
        for(int i=0;i<maxCount;i++)
        {
            if(newVals[i] != -1)
            {
                finalVals.Add(newVals[i]);
            }
        }

        for (int i = 0; i < finalVals.Count; i++)
        {
            finalVals[i] /= finalVals.Count;
            m_resultsMenu.values.Add(finalVals[i]);
            
        }

        m_resultsMenu.Draw(m_camera);



        /*
        for (int a = 0; a < m_playerResults.Count;a++)
        {
            float total = 0;
            PlayerData thisData = m_playerResults[a];

            for (int i=0;i<thisData.GetCount();i++)
            {
                if (thisData.m_results[i] == true)
                {
                    total += 1;
                }
            }
            total /= thisData.GetCount();
            m_resultsMenu.values.Add(total);
        }*/


    }

    public void TriggerCamera(bool active)
    {
        m_camera.gameObject.SetActive(active);
    }

    public void AddResult(string name, bool result)
    {
        bool exists = false;
        PlayerData thisData = new PlayerData() ;

        for (int i = 0; i < m_playerResults.Count; i++)
        {
            if(m_playerResults[i].m_name == name)
            {
                exists = true;
                m_playerResults[i].AddResult(result);
            }
        }

        if (!exists)
        {
            PlayerData tmp = new PlayerData();
            tmp.m_name = name;
            tmp.AddResult(result);

            m_playerResults.Add(tmp);
        }





    }

    public void SaveResults()
    {
        string folder = System.IO.Directory.GetCurrentDirectory();// + @"/Data";

        System.IO.StreamWriter file = new System.IO.StreamWriter(folder + @"\PlayerData.txt");
        file.Write("");
        file.Close();

        for (int i = 0; i < m_playerResults.Count; i++)
        {
            List<string> data = new List<string>();
            data.Add(m_playerResults[i].m_name); 
            for(int k = 0; k < m_playerResults[i].GetCount(); k++)
            {
                data.Add(",");
                data.Add(m_playerResults[i].m_results[k].ToString());
            }
            data.Add("\r\n");
            data.ToString();

            using (StreamWriter outputFile = new StreamWriter(folder + @"\PlayerData.txt", true))
            {
                foreach (string line in data)
                    outputFile.Write(line);
            }
        }







    }

    // Use this for initialization

    void Awake()
    {
        if(debug==true)
        {
            m_playerSpeed = 10f;
        }

        currentLevel = levels[0];

        m_currentNode = currentLevel.allPaths[0].transform.GetChild(0).GetComponent<Node>();
        m_currentPath = currentLevel.allPaths[0];
        m_currentTileIndex = 0;
        Debug.Log("Test controls - keys 1-6: move 1 to 6 tiles. B: move random number between 1-6");
        m_canMove = true;
        m_messageBox.SetActive(true);
        m_quizScreen.SetActive(true);
        m_crossroadsMsgBox.SetActive(false);

        Quiz a;
        a = new Quiz();
        a.name = "Environment";
        a.question = "How do Quendas help the environment?";
        a.quizNode1 = "Control introduced insect pests.";
        a.quizNode2 = "Benefit plant growth.";
        a.quizNode3 = "Quendas don't help the environment.";
        a.correctMessage = "Correct! Fungi in Quenda poo helps plants grow, and their digging may help increase water infiltration into soil.";
        a.incorrectMessage = "Incorrect. Fungi in Quenda poo helps plants grow, and their digging may help increase water infiltration into soil.";
        a.hint = "Hint: Quendas eat (and poop) underground insects, fungi and plant matter.";
        a.answer = 2;
        m_quizzes.Add(a);

        a = new Quiz();
        a.name = "Water";
        a.question = "You wonder if you should go search for water. How often do Quendas need water?";
        a.quizNode1 = "Often - Quendas need to live in an area with frequent sources of fresh water.";
        a.quizNode2 = "Not often, they get most of the hydration they need from food.";
        a.correctMessage = "Correct! Quendas get most of the hydration they need from food.";
        a.incorrectMessage = "Incorrect. Quendas get most of the hydration they need from food.";
        a.hint = " ";
        a.answer = 2;
        m_quizzes.Add(a);
        /*
        a = new Quiz();
        a.name = "Garden";
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
        */
        /*
        a = new Quiz();
        a.name = "Active";
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
        */

        a = new Quiz();
        a.name = "Missing";
        a.question = "You find a quenda missing some fur and their tail. You should figure out why in case there is danger nearby. Why might a Quenda be missing their fur or tail?";
        a.quizNode1 = "They shed and regrow their fur and tail.";
        a.quizNode2 = "Males can fight for territory and mating.";
        a.correctMessage = "Correct! Males can fight for territory and mating.";
        a.incorrectMessage = "Incorrect. Males can fight for territory and mating.";
        a.hint = " ";
        a.answer = 2;
        m_quizzes.Add(a);




        a = new Quiz();
        a.name = "Dig";
        a.hint = "Hint: Quendas are marsupials that eat subterranean plants, insects and fungi.";
        a.question = "You want to decide if you should dig a hole. Why do Quendas dig holes?";
        a.quizNode1 = "Provide nest for young";
        a.quizNode2 = "Search for food";
        a.quizNode3 = "Sharpen their claws";
        a.correctMessage = "Correct! Quendas dig cone-shaped holes to search for food.";
        a.incorrectMessage = "Incorrect. ";
        a.answer = 2;
        m_quizzes.Add(a);
        
        a = new Quiz();
        a.name = "Eat";
        a.hint = "Hint: Quendas dig for food.";
        a.question = "You find some food, but are not sure if it's safe to eat. What do Quendas mainly eat?";
        a.quizNode1 = "Fruit and nuts";
        a.quizNode2 = "Insects and fungi";
        a.quizNode3 = "Food scraps";
        a.quizNode4 = "Animal remains";
        a.correctMessage = "Correct! Quendas eat insects, fungi, and underground plant matter.";
        a.incorrectMessage = "Incorrect. Quendas eat insects, fungi, and underground plant matter.";
        a.answer = 2;
        m_quizzes.Add(a);

        a = new Quiz();
        a.name = "Age";
        a.question = "How long do Quendas live?";
        a.quizNode1 = "~1-2 years";
        a.quizNode2 = "~3-5 years";
        a.quizNode3 = "~8-10 years";
        a.correctMessage = "Correct! Quendas live 3-5 years.";
        a.incorrectMessage = "Incorrect. Quendas live 3-5 years.";
        a.answer = 2;
        m_quizzes.Add(a);

        a = new Quiz();
        a.name = "Cat";
        a.question = "In what scenario are cats likely to be dangerous to Quendas?";
        a.quizNode1 = "If they are allowed to roam freely";
        a.quizNode2 = "If they are too friendly or curious";
        a.quizNode3 = "If they mainly stay indoors or in your garden";
        a.correctMessage = "Correct! Roaming cats are dangerous predators to Quendas.";
        a.incorrectMessage = "Incorrect. Roaming cats are dangerous predators to Quendas.";
        a.answer = 1;
        m_quizzes.Add(a);

        a = new Quiz();
        a.name = "Dog";
        a.question = "True or False - Quendas can smell where dogs have been, and avoid these areas, which limits where they can find food.";
        a.quizNode1 = "True";
        a.quizNode2 = "False";
        a.correctMessage = "Correct! Quendas avoid the smell of dogs.";
        a.incorrectMessage = "Incorrect. Quendas do avoid the smell of dogs.";
        a.answer = 1;
        m_quizzes.Add(a);


        a = new Quiz();
        a.name = "Threat";
        a.question = "You see something that could be dangerous, but you're not sure. Which of these is not a major threat to Quendas?";
        a.quizNode1 = "Predators such as foxes, cats and dogs";
        a.quizNode2 = "Becoming dependent on humans for food";
        a.quizNode3 = "Drivers on roads passing through Quenda habitats";
        a.quizNode4 = "Parks in residential areas";
        a.correctMessage = "Correct! Parks in residential areas do not provide a threat to Quendas.";
        a.incorrectMessage = "Incorrect. Parks in residential areas do not provide a threat to Quendas.";
        a.answer = 4;
        m_quizzes.Add(a);


        a = new Quiz();
        a.name = "Food2";
        a.question = "Which of the following foods is good for Quendas?";
        a.quizNode1 = "Cheese";
        a.quizNode2 = "Peanuts";
        a.quizNode3 = "Fungi";
        a.quizNode4 = "Meat";
        a.correctMessage = "Correct! Quendas eat underground fungi.";
        a.incorrectMessage = "Incorrect.  Quendas eat underground fungi.";
        a.answer = 3;
        m_quizzes.Add(a);

        a = new Quiz();
        a.name = "humanoffer";
        a.question = "A human approaches and offers you some food. What should you do?";
        a.quizNode1 = "Accept the food";
        a.quizNode2 = "Run away";
        a.correctMessage = "Correct! If Quendas become dependent on humans as a source of food they could starve if you leave. You could also risk feeding Quendas something dangerous without knowing.";
        a.incorrectMessage = "Incorrect. If Quendas become dependent on humans as a source of food they could starve if you leave.You could also risk feeding Quendas something dangerous without knowing.";
        a.answer = 2;
        m_quizzes.Add(a);


        a = new Quiz();
        a.name = "road";
        a.question = "You come across a large, flat stone area. What should you do?";
        a.quizNode1 = "Look around";
        a.quizNode2 = "Stay away";
        a.correctMessage = "Correct! This could be a road. Drivers should watch out for Quendas crossing the road in natural areas.";
        a.incorrectMessage = "Incorrect. This could be a road. Drivers should watch out for Quendas crossing the road in natural areas.";
        a.answer = 2;
        m_quizzes.Add(a);

        a = new Quiz();
        a.name = "shrub";
        a.question = "You see a large area of thick, low shrubland. What should you do?";
        a.quizNode1 = "Approach";
        a.quizNode2 = "Stay away";
        a.correctMessage = "Correct! Thick, low shrubland is an ideal habitat for Quendas, providing them with food and safety.";
        a.incorrectMessage = "Incorrect. Thick, low shrubland is an ideal habitat for Quendas, providing them with food and safety.";
        a.answer = 1;
        m_quizzes.Add(a);

        /*
        a = new Quiz();
        a.name = "";
        a.hint = "Hint: ";
        a.question = "";
        a.quizNode1 = "";
        a.quizNode2 = "";
        a.quizNode3 = "";
        a.quizNode4 = "";
        a.correctMessage = "Correct! ";
        a.incorrectMessage = "Incorrect. ";
        a.answer = ;
        m_quizzes.Add(a);
        */
        m_playerMesh.SetActive(false);

    }
    void Start()
    {
        m_crossroadsMsgBox.SetActive(false);
        messageBox.gameObject.SetActive(false);
        m_quizScreen.SetActive(false);
        m_creditsMenu.SetActive(false);
        m_resultsMenu.gameObject.SetActive(false);
        m_overlayUI.SetActive(false);
        m_playerMesh.SetActive(false);
        m_mainMenu.SetActive(true);
        ShowMainMenu();



    }

    public void StartNewGame()
    {
        LoadGame(0, 0, 0);
        messageBox.DisplayMessageBox("Your name is Ken the Quenda, and you have to pass through this area to reach a new place to live. Try to make it all the way to the end, tackling the challenges you will find along the way.");

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
        currentPath = currentLevel.allPaths[index];
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
        currentPath = currentLevel.allPaths[index];
        currentPathIndex = index;
        SetCurrentNode(0);

        if (m_tilesToMove > 0)
        {

            m_playerMesh.transform.LookAt(m_currentNode.transform, new Vector3(0, 1, 0));


            SetPlayerAnimation(true);
            
            if (m_tilesToMove>currentPath.transform.childCount)
            {
                Debug.Log("Capping movement to new path's end.");
                m_tilesToMove = currentPath.transform.childCount;

            }
            //m_movingOnPath = true;
        }
        else
        {
            if(m_tilesToMove == 0)
            {
                AllowStartMovement();
            }
        }
        
        // Shouldnt be <0 tiles to move because you're just entering this path
        // at the start.
    }

    /// <summary>
    /// Moves to a new path, starting on its last node.
    /// </summary>
    /// <param name="index">Path index.</param>
    public void SetCurrentPathAtEnd(int index)
    {
        oldIndex = currentPathIndex;
        Debug.Log("Setting new path at end.");
        currentPath = currentLevel.allPaths[index];
        currentPathIndex = index;
        SetCurrentNode(currentPath.transform.childCount - 1);

        if (m_tilesToMove < 0)
        {
            //if (lastNode.type == Node.NodeType.none)
            //{
            Debug.Log("Setting current path at end");

            m_tilesToMove = -currentPath.transform.childCount + (m_tilesToMove + 1);
            //}
            m_movingOnPath = true;
        }
    }

    public void SaveGame()
    {
        string[] lines = new string[10];
        lines[0] = currentLevel.id.ToString();
        lines[1] = currentPathIndex.ToString();
        lines[2] = m_currentTileIndex.ToString();
        string mytext = lines[0]+ System.Environment.NewLine+lines[1]+ System.Environment.NewLine+lines[2]+ System.Environment.NewLine;

        
        TextAsset asset = Resources.Load("save.txt") as TextAsset;
        writer = new StreamWriter("Resources/" + "save.txt");
        writer.WriteLine(mytext);

        writer.Close();

    }
    private StreamWriter writer;
    private StreamReader reader;

    [SerializeField]
    private Level m_currentLevel;

    public Level currentLevel { get { return m_currentLevel; }set { m_currentLevel = value; } }

    private int m_levelToMoveTo = -1;

    public void ReadyMoveToLevel(int levelIndex)
    {
        m_levelToMoveTo = levelIndex;
    }

    public void ShowSaveMsgBox()
    {
        m_saveMsgBox.SetActive(true);
    }
    public void LoadGame(int levelIndex, int pathIndex, int tileIndex)
    {

        if(levelIndex == 0)
        {
            levels[1].gameObject.SetActive(false);
        }
        else
        {
            if (levelIndex == 1)
            {
                levels[0].gameObject.SetActive(false);
            }
        }
        m_paused = false;
        messageBox.gameObject.SetActive(true);


        m_quizScreen.SetActive(true);

        m_playerMesh.SetActive(true);
        m_overlayUI.SetActive(true);
        CloseCrossroadsMsgBox();
        CloseSaveMsgBox();
        messageBox.CloseMessageBox();
        m_mainMenu.SetActive(false);

        currentLevel = levels[levelIndex];

        currentPathIndex = pathIndex;
        m_currentPath = currentLevel.allPaths[currentPathIndex];
        m_currentTileIndex = tileIndex;


        m_currentNode = currentLevel.allPaths[currentPathIndex].transform.GetChild(m_currentTileIndex).GetComponent<Node>();


        m_player.transform.position = m_currentNode.transform.position;
        levels[levelIndex].gameObject.SetActive(true);

        AllowStartMovement();

    }


    public void LoadGame()
    {

        TextAsset asset = Resources.Load("save.txt") as TextAsset;
        reader = new StreamReader("Resources/" + "save.txt");


        LoadGame(int.Parse(reader.ReadLine()), int.Parse(reader.ReadLine()), int.Parse(reader.ReadLine()));

        reader.Close();


    }

    public void MoveToLevel2()
    {
        currentLevel = levels[1];
    }

    [SerializeField]
    private List<Level> levels;



    /// <summary>
    /// Moves to node of given index on the current path.
    /// </summary>
    /// <param name="index">Node index.</param>
    public void SetCurrentNode(int index)
    {
        currentNode = currentPath.transform.GetChild(index).GetComponent<Node>();
        m_currentTileIndex = index;

    }



    //private List<ListWrapper> m_allPaths = new List<ListWrapper>();

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
        if(m_playerMesh.activeSelf)
        {

            if (player.GetComponentInChildren<Animator>().enabled != state)
            {
                player.GetComponentInChildren<Animator>().enabled = state;
            }
        }
    }

    bool waitingForTileAction = false;

    // Update is called once per frame
    void Update()
    {

        if(m_levelToMoveTo != -1)
        {
            if(!messageBox.Active() && !m_saveMsgBox.activeSelf)
            {

                switch(m_levelToMoveTo)
                {
                    case 1:
                        LoadGame(m_levelToMoveTo, 0, 0);

                        break;
                    case 2:
                        ShowMainMenu();

                        break;


                    case 11:
                        // cat game
                        scene.SetActive(false);
                        TriggerCamera(false);
                        DisallowStartMovement();
                        LoadScene(0, 1, true);

                        break;


                    case 12:
                        // food game
                        scene.SetActive(false);
                        TriggerCamera(false);
                        DisallowStartMovement();
                        LoadScene(0, 5, true);

                        break;

                    case 13:
                        // card game
                        scene.SetActive(false);
                        TriggerCamera(false);
                        DisallowStartMovement();
                        LoadScene(0, 6, true);

                        break;

                }
                    
                m_levelToMoveTo = -1;

            }

        }

        else
        {
            if (!m_paused)
            {
                if (waitingForTileAction)
                {
                    if (!messageBox.Active())
                    {
                        m_movedTiles = 0;
                        m_movingOnPath = false;
                        m_currentNode.PerformAction();
                        waitingForTileAction = false;
                        AllowStartMovement();

                    }
                }
                else
                {
                    if (m_player.transform.position != m_currentNode.transform.position)
                    {
                        MoveToNode(m_currentNode);
                    }
                    else
                    {
                        if (m_movingOnPath)
                        {
                            Debug.Log(m_tilesToMove + "tiles to move.");
                            if (m_tilesToMove != 0)
                            {
                                if (m_tilesToMove < 0)
                                {
                                    Debug.Log("Moving back.");
                                    m_tilesToMove += 1;
                                    GoToPrevNodeWithoutEvent();

                                }
                                else
                                {
                                    Debug.Log("Moving forward.");
                                    m_tilesToMove -= 1;
                                    GoToNextNodeWithoutEvent();

                                }
                                if (m_currentNode.type == Node.NodeType.crossroads)
                                {
                                    if (messageBox.Active())
                                    {
                                        waitingForTileAction = true;
                                    }
                                    else
                                    {
                                        m_movedTiles = 0;
                                        m_movingOnPath = false;
                                        m_currentNode.PerformAction();
                                    }
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
                            }
                            else
                            {
                                SetPlayerAnimation(false);

                                if (!messageBox.Active())
                                {

                                    

                                    m_movedTiles = 0;
                                    m_movingOnPath = false;
                                    m_currentNode.PerformAction();
                              
                                    if(m_currentNode.type == Node.NodeType.end)
                                    {
                                        AllowStartMovement();
                                    }


                                }
                                else
                                {

                                    waitingForTileAction = true;
                                    // Stops message box from resuming play, as there is a tile action in queue that
                                    // will not allow the player to move until it is complete.
                                    if (messageBox.canMoveOnResume)
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

                        if (Input.GetKeyDown(KeyCode.Alpha7))
                        {

                            MoveOnPath(100);
                        }
                    }
                    


                }

            }
            else
            {
                SetPlayerAnimation(false);

            }
        }

        // Player movement
        /*
        if (!(m_player.transform.position == m_currentNode.transform.position))
        {
            MoveToNode(m_currentNode);
        }
        */

        // BUG: Doesn't go if tiles to move more than count
        // Instead just make it set tiles to move to count when you set this.
        

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

    public void ShowMainMenu()
    {
        SaveResults();
        m_crossroadsMsgBox.SetActive(false);
        messageBox.gameObject.SetActive(false);
        paused = true;
        m_quizScreen.SetActive(false);
        for(int i=0;i<levels.Count;i++)
        {
            levels[i].gameObject.SetActive(false);
        }
        m_creditsMenu.SetActive(false);
        m_resultsMenu.gameObject.SetActive(false);
        m_overlayUI.SetActive(false);
        m_playerMesh.SetActive(false);
        m_mainMenu.SetActive(true);
    }

    [SerializeField]
    GameObject m_mainMenu;

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
            
            Node lastNode = currentPath.transform.GetChild(currentPath.transform.childCount - 1).GetComponent<Node>();
            if ((m_tilesToMove > (currentPath.transform.childCount - (currentTileIndex + 1)) && (lastNode.type == Node.NodeType.end)))
            {
                Debug.Log("Limiting to end of path.");
                m_tilesToMove = (currentPath.transform.childCount - (currentTileIndex + 1));
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
        m_paused = false;
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

    public void CloseSaveMsgBox()
    {
        m_saveMsgBox.SetActive(false);
    }

    [SerializeField]
    private GameObject m_saveMsgBox;


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

    public void Quit()
    {
        Application.Quit();
    }
    
}