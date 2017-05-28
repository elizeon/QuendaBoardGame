using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
/// <summary>
/// This is the main type for your game.
/// </summary>
public class CatGame : MonoBehaviour

{


    [SerializeField]
    public GameObject m_catGame;

    [SerializeField]
    public float m_playerMoveSpeedEasy;

    [SerializeField]
    public float m_enemyMoveSpeedEasy;


    [SerializeField]
    public float m_playerMoveSpeedMedium;

    [SerializeField]
    public float m_enemyMoveSpeedMedium;


    [SerializeField]
    public float m_playerMoveSpeedHard;

    [SerializeField]
    public float m_enemyMoveSpeedHard;

    [SerializeField]
    GameObject m_layout1;

    Game m_game;



    /*
     * [SerializeField]
    GameObject m_grassObj;
    [SerializeField]
    GameObject m_bush;
    [SerializeField]
    GameObject m_player;
    [SerializeField]
    GameObject m_enemy;
     
    */
    [SerializeField]
    List<TextAsset> m_enemyPaths;

    [SerializeField]
    GameObject m_camera;
    Camera m_camerac;
    Player m_playerp;
    public Player player { get { return m_playerp; } }



    // Use this for initialization
    void Start ()
    {
        m_game = FindObjectOfType<Game>();
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(m_camera);

        DontDestroyOnLoad(m_layout1);
        //DontDestroyOnLoad(m_enemy);
        //DontDestroyOnLoad(m_bush);

        //

        m_camerac = m_camera.GetComponent<Camera>();
        /*
        m_enemy1.AddCollisionTrigger(m_player, OtherTakesDamageStop);
        m_enemy2.AddCollisionTrigger(m_player, OtherTakesDamageStop);
        m_enemy3.AddCollisionTrigger(m_player, OtherTakesDamageStop);
        m_enemy4.AddCollisionTrigger(m_player, OtherTakesDamageStop);
        m_player.AddCollisionTrigger(m_end, PassLevel);
        */



        if (m_debugMode)
        {
            m_debugPath = new List<Vector2>();
        }

        //Console.WriteLine("Initialising...");

        //base.Initialize();

        levelStart = false;
        /*
        graphics.PreferredBackBufferHeight=500;
        graphics.PreferredBackBufferWidth=1000;
        graphics.ApplyChanges();
        */

        //Render2D.Instance.Init(GraphicsDevice);

        
        m_grid = GetComponent<GameGrid>();

        m_grid.Set(10, 10, 0.1f);


        Level1Init();
    }
	
	// Update is called once per frame
	void Update ()
    {
            
            // Dev tool for making enemy paths
            if (m_debugMode)
            {
                
                // Add a point

                if (Input.GetMouseButtonDown(0))
                {
                    m_debugPath.Add(Input.mousePosition);
                }


                // Clear the list of points
                if (Input.GetKeyDown(KeyCode.K))
                {
                    m_debugPath = new List<Vector2>();
                }
                
            }

            

            /*
            if (currentScene == 3 || currentScene == 4)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartGame();
                }
            }

            if (currentScene == 5)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {

                    StartGame();
                }


                if (Input.GetMouseButtonDown(0))
                {

                    if (_2DUtil.CheckCollision(m_btnEasy.boundingBox, Input.mousePosition))
                    {

                        difficulty = 0;
                        m_btnEasy.SetAnim("Down");
                        m_btnMedium.SetAnim("Up");
                        m_btnHard.SetAnim("Up");
                    }

                    if (_2DUtil.CheckCollision(m_btnMedium.boundingBox, Input.mousePosition))
                    {
                        difficulty = 1;
                        m_btnMedium.SetAnim("Down");
                        m_btnHard.SetAnim("Up");
                        m_btnEasy.SetAnim("Up");

                    }
                    if (_2DUtil.CheckCollision(m_btnHard.boundingBox, Input.mousePosition))
                    {
                        difficulty = 2;
                        m_btnHard.SetAnim("Down");
                        m_btnMedium.SetAnim("Up");
                        m_btnEasy.SetAnim("Up");

                    }

                    if (_2DUtil.CheckCollision(m_btnStart.boundingBox, Input.mousePosition))
                    {

                        StartGame();

                    }
                    
                }

            if (currentScene == 3)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_2DUtil.CheckCollision(m_btnRestart.boundingBox, Input.mousePosition))
                    {

                        EnterScene(m_currentLevel);

                    }

                    if (_2DUtil.CheckCollision(m_btnQuit.boundingBox, Input.mousePosition))
                    {
                        EnterScene(5);

                    }


                }
            }
            if (currentScene == 4)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    if (_2DUtil.CheckCollision(m_btnRestart.boundingBox, Input.mousePosition))
                    {
                        StartGame();

                    }

                    if (_2DUtil.CheckCollision(m_btnQuit.boundingBox, Input.mousePosition))
                    {
                        EnterScene(5);

                    }
                }
            }
            if (currentScene == 6)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_2DUtil.CheckCollision(m_btnContinue.boundingBox, Input.mousePosition))
                    {
                        m_currentLevel += 1;
                        EnterScene(m_currentLevel);

                    }
                }
            }

        }
        else
        {
            ReturnToGame();
        }
        if (Input.GetMouseButtonDown(0))
        {
            ReturnToGame();
        }
        */
        
    }


    public void ReturnToGame(int currentScene)
    {
        // todo add level to file->build settings
        //m_game.LoadScene(currentScene, 0, false);
        Destroy(m_catGame);

        m_game.scene.SetActive(true);

        m_game.TriggerCamera(true);
        m_game.AllowStartMovement();

        string str;

        if(m_result)
        {
            m_game.messageBox.DisplayMessageBox("Success! Move forward 3 spaces.",false);
            m_game.MoveOnPath(3);
            m_game.DisallowStartMovement();

            m_game.AddResult("Cat Game", m_result);
            m_game.SaveResults();
        }
        else
        {
            m_game.messageBox.DisplayMessageBox("The cat spotted you and chased you out of the area. You failed. Move backwards 3 spaces.",false);
            m_game.MoveOnPath(-3);
            m_game.DisallowStartMovement();

            m_game.AddResult("Cat Game", m_result);
            m_game.SaveResults();
        }




    }


        #region Variables


        // Debug

        /// <summary>
        /// Debug mode
        /// </summary>
        private bool m_debugMode = false;

        // Settings

        /// <summary>
        /// Difficulty, 0=easy, 1=med, 2=hard
        /// </summary>
        public int difficulty = 1;


        // Content directories

        /// <summary>
        /// Texture directory
        /// </summary>
        string texDir = "Content/textures/";

        // Utility variables
        Vector2 baseVec = new Vector2(1, 1);

        // Time
        public static float deltaTime { get { return m_deltaTime; } }
        private static float m_deltaTime = 0;

        public static float totalTime { get { return m_totalTime; } }
        private static float m_totalTime;


        /// <summary>
        /// Has the scene only just loaded
        /// </summary>
        public static bool levelStart { get; set; }


        // Input

        //public static MouseState mouseState { get; private set; }
        //public static KeyboardState keyState { get; private set; }
        //public static KeyboardState oldKeyState { get; private set; }

        // Graphics
        //GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;
        public static Texture2D whiteTex { get; private set; }
        //public static Matrix viewMatrix;

        
        GameGrid m_grid;
        public GameGrid gameGrid { get { return m_grid; } }


        #region Global Scenes & GameObjects
        // Gameobjects
        /*
        HideLoc m_bush1 = new HideLoc("bush1", "hideloc", 0);
        HideLoc m_bush2 = new HideLoc("bush2", "hideloc", 0);
        HideLoc m_bush3 = new HideLoc("bush3", "hideloc", 0);
        HideLoc m_bush4 = new HideLoc("bush4", "hideloc", 0);
        HideLoc m_bush5 = new HideLoc("bush5", "hideloc", 0);
        */
        //Enemy m_enemy1 = new Enemy("enemy1", "enemy", 0 ,1);
        //Enemy m_enemy2 = new Enemy("enemy1", "enemy", 0 ,1);
        //Enemy m_enemy3 = new Enemy("enemy1", "enemy", 0,1);
        //Enemy m_enemy4 = new Enemy("enemy1", "enemy", 0,1);

        /*
        GameObject2D m_end = new GameObject2D("end", "end");



        // UI Buttons
        GameObject2D m_btnEasy = new GameObject2D("buttoneasy", "button");
        GameObject2D m_btnMedium = new GameObject2D("buttonmed", "button");
        GameObject2D m_btnHard = new GameObject2D("buttonhard", "button");
        GameObject2D m_btnStart = new GameObject2D("buttonstart", "button");
        GameObject2D m_btnContinue = new GameObject2D("buttonstart", "button");
        GameObject2D m_btnQuit = new GameObject2D("buttonquit", "button");
        GameObject2D m_btnRestart = new GameObject2D("buttonrestart", "button");
        */
    // Scenes
    /*
            Scene m_scene1 = new Scene();
            Scene m_scene2 = new Scene();
            Scene m_scene3 = new Scene();
            Scene m_winScene = new Scene();
            Scene m_loseScene = new Scene();
            Scene m_nextLevelScene = new Scene();

        
            static List<Scene> m_scenes = new List<Scene>();
            public static List<Scene> scenes { get { return m_scenes; } }
             */

    // Progress tracking
    public static int currentScene { get; private set; }
           
    int m_levelsPassed = 0;
        int m_totalLevels = 3;
        private int m_currentLevel = 0;

        #endregion

        #endregion

        

        //public static MouseState oldMouseState { get; private set; }


        private List<Vector2> m_debugPath;

    

    /// <summary>
    /// The scene switches to losing screen.
    /// </summary>
    public void Lose()
    {
        m_result = false;
        ReturnToGame(m_currentLevel + 1);
        //m_game.LoadScene(m_currentLevel+1,4,true);
    }

    /// <summary>
    /// The scene switches to winning screen.
    /// </summary>
    public void Win()
    {
        m_result = true;

        ReturnToGame(m_currentLevel + 1);

        //m_game.LoadScene(m_currentLevel + 1, 3, true);
    }

    bool m_result;



    /// <summary>
    /// Load and start the scene of chosen index.
    /// </summary>
    /// <param name="scn">Index of scene in m_scenes.</param>
    /// 
    /*
    public void EnterScene(int scn)
        {

        Application.LoadLevel(scn);
            //m_scenes[currentScene].ClearCollisionEvents();


            GlobalObjectsInit();

            //Console.WriteLine("Scene " + scn + " loaded.");
            levelStart = true;

            switch (scn)
            {
                case 0:
                    m_currentLevel = 0;
                    m_levelsPassed = 0;
                    Level1Init();


                    break;
                case 1:
                    m_currentLevel = 1;
                    m_levelsPassed = 1;
                    Level2Init();
                    break;
                case 2:
                    m_currentLevel = 2;
                    m_levelsPassed = 2;
                    Level3Init();
                    break;
            }

            currentScene = scn;
        }


        void StartGame()
        {
            m_currentLevel = 0;
            EnterScene(0);
        }
    */

    /// <summary>
    /// Initialise global objects before starting scenes.
    /// </summary>
    void GlobalObjectsInit()
        {
        }

        /// <summary>
        /// Write list of vector2 to file
        /// </summary>
        /// <param name="vec2s">List of vector2 to write</param>
        /// <param name="filepath">File path</param>
        void WriteVec2sToFile(List<Vector2> vec2s, string filepath)
        {
            string[] lines = new string[m_debugPath.Count];
            for (int i = 0; i < m_debugPath.Count; i++)
            {
                lines[i] = m_debugPath[i].x + "," + m_debugPath[i].y;
            }

            System.IO.File.WriteAllLines(filepath, lines);
        }

        /// <summary>
        ///  Load series of vec2 in screen space to game space
        /// </summary>
        /// <param name="filepath">File path to write to</param>
        /// <returns>List of Vector2 read from file</returns>
        List<Vector2> LoadVec2GridPathFromFile(TextAsset file)
        {
        string read = file.text;
        string [] lines = read.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None);
            List<Vector2> newVec = new List<Vector2>();  

            for (int i = 0; i < lines.Length-1; i++)
            {
                string[] thisLine = lines[i].Split(',');
                newVec.Add(m_grid.GetPoint(float.Parse(thisLine[0]) / Screen.width, float.Parse(thisLine[1]) / Screen.height));
            } 
        return newVec;
           
    }

        /// <summary>
        /// Enemy speed on easy
        /// </summary>
        float m_easySpeed = 0.07f;
        /// <summary>
        /// Enemy speed on normal
        /// </summary>
        float m_normalSpeed = 0.1f;
        /// <summary>
        /// Enemy speed on hard
        /// </summary>
        float m_hardSpeed = 0.2f;


    /// <summary>
    /// Init level 1
    /// </summary>
    void Level1Init()
        {

        m_game.LoadScene(1,2,true);

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(m_layout1);
      
        //SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName("CatGame"));
        //SceneManager.MoveGameObjectToScene(m_layout1, SceneManager.GetActiveScene());
        //SceneManager.MoveGameObjectToScene(gameObject, nextScene);

        //GameObject[] goArray = SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();


        //SceneManager.MoveGameObjectToScene(m_enemy, SceneManager.GetActiveScene());
        //SceneManager.MoveGameObjectToScene(m_player, SceneManager.GetActiveScene());

        //m_playerp.TriggerLife();
        //m_playerp.SetPos2D(m_grid.GetPointWorld(m_camerac, 0.1f, 0.1f));
        //m_playerp.TriggerLife();
        /*
        HideLoc m_bush1 = Instantiate(m_bush).GetComponent<HideLoc>();
        m_bush1.gameObject.SetActive(true);
        HideLoc m_bush2 = Instantiate(m_bush).GetComponent<HideLoc>();
        m_bush2.gameObject.SetActive(true);

        HideLoc m_bush3 = Instantiate(m_bush).GetComponent<HideLoc>();
        m_bush3.gameObject.SetActive(true);

        HideLoc m_bush4 = Instantiate(m_bush).GetComponent<HideLoc>();
        m_bush4.gameObject.SetActive(true);

        */
        /*
        m_bush1.SetPos2D(m_grid.GetPointWorld(m_camerac, 0.1f, 0.7f));
        m_bush2.SetPos2D(m_grid.GetPointWorld(m_camerac,0.3f, 0.3f));
        m_bush3.SetPos2D(m_grid.GetPointWorld(m_camerac,0.6f, 0.6f));
        m_bush4.SetPos2D(m_grid.GetPointWorld(m_camerac,0.8f, 0.4f));



        

        //GameObject m_player1 = Instantiate(m_player);
        //Player m_player1p = m_player1.GetComponent<Player>();

        m_playerp.SetPos2D(m_grid.GetPointWorld(m_camerac, 0.1f, 0.1f));

        //m_player = m_player1;
        //m_playerp = m_player1p;

        

        List<Vector2> patrolPath = //new List<Vector2>(); patrolPath.Add(m_grid.GetPoint(0.9f, 0.9f));
        LoadVec2GridPathFromFile(m_enemyPaths[0]);
        m_enemy1e.Set("enemy", "enemy", 10, 1f,10f);

        for (int i=0;i<patrolPath.Count;i++)
        {
            Vector2 world = m_camerac.ScreenToWorldPoint(new Vector3(patrolPath[i].x,patrolPath[i].y,1));
            patrolPath[i] = world;
        }

        m_enemy1e.patrolPath = patrolPath;
        m_enemy1e.SetEnemyState(Enemy.EnemyState.patrolling);
        m_enemy1e.StartPatrol(1);

        m_enemy1e.SetPos2D(m_grid.GetPointWorld(m_camerac,0.3f, 0.2f));

        m_enemy1.SetActive(true);
        */

        m_playerp = m_layout1.GetComponentInChildren<Player>();
        m_playerp.Set("player", "player", 10);

        Enemy m_enemy1e = m_layout1.GetComponentInChildren<Enemy>();



        //List<Vector2> patrolPath = //new List<Vector2>(); patrolPath.Add(m_grid.GetPoint(0.9f, 0.9f));
        //LoadVec2GridPathFromFile(m_enemyPaths[0]);
        m_enemy1e.Set("enemy", "enemy", 10, 1f, 10f,Enemy.EnemyState.patrolling);
        /*
        for (int i = 0; i < patrolPath.Count; i++)
        {
            Vector2 world = m_camerac.ScreenToWorldPoint(new Vector3(patrolPath[i].x, patrolPath[i].y, 1f));
            patrolPath[i] = world;
        }
        */
        //m_enemy1e.SetEnemyState(Enemy.EnemyState.patrolling);
        m_enemy1e.StartPatrol(1);

        // todo remove
        
        if(difficulty == 0)
        {
            m_enemy1e.SetMoveSpeed(m_enemyMoveSpeedEasy * 1000);
            m_enemy1e.viewDist = Utility.Max(Screen.width/4, Screen.height/4); ;
            m_playerp.GetComponent<Movement>().speed = (m_playerMoveSpeedEasy * 1000);
        }

        if (difficulty == 1)
        {
            m_enemy1e.SetMoveSpeed(m_enemyMoveSpeedEasy * 1600);
            m_enemy1e.viewDist = Utility.Max(Screen.width/2, Screen.height/2); ;
            m_playerp.GetComponent<Movement>().speed = (m_playerMoveSpeedEasy * 1600);
        }


        if (difficulty == 2)
        {
            m_enemy1e.SetMoveSpeed(m_enemyMoveSpeedEasy * 2500);
            m_enemy1e.viewDist = Utility.Max(Screen.width, Screen.height); ;
            m_playerp.GetComponent<Movement>().speed = (m_playerMoveSpeedEasy * 2500);
        }

        // m_player1.SetActive(true);



        /*
            m_scene1 = new Scene();

            m_player.TriggerLife();

            m_scene1.AddObject(m_grassObj);

            m_bush1.SetPos2D(m_grid.GetPoint(0.1f, 0.7f));

            m_bush2.SetPos2D(m_grid.GetPoint(0.3f, 0.3f));
            m_bush3.SetPos2D(m_grid.GetPoint(0.6f, 0.6f));
            m_bush4.SetPos2D(m_grid.GetPoint(0.8f, 0.4f));



            Shadow playerShadow = new Shadow("pshadow", "shadow", m_player);
            playerShadow.AddAnimSprite("shadow", m_shadow);
            playerShadow.SetAnim("shadow");
            playerShadow.scale = new Vector2(0.11f, 0.11f);

            m_scene1.AddObject(playerShadow);

            m_scene1.AddObject(m_player);



            List<Vector2> patrolPath = //new List<Vector2>(); patrolPath.Add(m_grid.GetPoint(0.9f, 0.9f));
            LoadVec2GridPathFromFile("enemy1.txt");


            m_enemy1.patrolPath = patrolPath;
            m_enemy1.SetEnemyState(Enemy.EnemyState.patrolling);
            m_enemy1.StartPatrol(1);


            Shadow enemy1Shadow = new Shadow("e1shadow", "shadow", m_enemy1);
            enemy1Shadow.AddAnimSprite("shadow", m_shadow);
            enemy1Shadow.SetAnim("shadow");
            enemy1Shadow.scale = new Vector2(0.3f, 0.3f);
            m_scene1.AddObject(enemy1Shadow);


            m_scene1.AddObject(m_enemy1);

            m_scene1.AddObject(m_bush1);
            m_scene1.AddObject(m_bush2);
            m_scene1.AddObject(m_bush3);
            m_scene1.AddObject(m_bush4);


            m_scene1.AddObject(m_end);
            m_scenes[0] = m_scene1;

        */
    }


    /// <summary>
    /// Init level 2
    /// </summary>
    void Level2Init()
        {
        /*
            m_scene2 = new Scene();

            m_scene2.AddObject(m_grassObj);


            m_bush1.SetPos2D(m_grid.GetPoint(0.1f, 0.5f));
            m_bush2.SetPos2D(m_grid.GetPoint(0.35f, 0.9f));
            m_bush3.SetPos2D(m_grid.GetPoint(0.6f, 0.4f));



            m_player.SetPos2D(m_grid.GetPoint(0.1f, 0.5f));




            Shadow playerShadow = new Shadow("pshadow", "shadow", m_player);
            playerShadow.AddAnimSprite("shadow", m_shadow);
            playerShadow.SetAnim("shadow");
            playerShadow.scale = new Vector2(0.11f, 0.11f);
            m_scene2.AddObject(playerShadow);


            m_enemy2.SetPos2D(m_grid.GetPoint(0.5f, 0.5f));
            m_enemy2.SetEnemyState(Enemy.EnemyState.patrolling);


            Shadow enemy2Shadow = new Shadow("e2shadow", "shadow", m_enemy2);
            enemy2Shadow.AddAnimSprite("shadow", m_shadow);
            enemy2Shadow.SetAnim("shadow");
            enemy2Shadow.scale = new Vector2(0.3f, 0.3f);
            m_scene2.AddObject(enemy2Shadow);



            m_enemy3.SetPos2D(m_grid.GetPoint(0.4f, 0.1f));
            m_enemy3.SetEnemyState(Enemy.EnemyState.patrolling);


            Shadow enemy3Shadow = new Shadow("e3shadow", "shadow", m_enemy3);
            enemy3Shadow.AddAnimSprite("shadow", m_shadow);
            enemy3Shadow.SetAnim("shadow");
            enemy3Shadow.scale = new Vector2(0.3f, 0.3f);
            m_scene2.AddObject(enemy3Shadow);



            List<Vector2> patrolPath;

            patrolPath = LoadVec2GridPathFromFile("enemy2.txt");
            m_enemy2.patrolPath = patrolPath;

            patrolPath = LoadVec2GridPathFromFile("enemy3.txt");
            m_enemy3.patrolPath = patrolPath;

            m_enemy2.StartPatrol(0);
            m_enemy3.StartPatrol(0);





            m_player.TriggerLife();
            m_scene2.AddObject(m_player);




            m_scene2.AddObject(m_enemy2);
            m_scene2.AddObject(m_enemy3);


            m_scene2.AddObject(m_bush1);
            m_scene2.AddObject(m_bush2);
            m_scene2.AddObject(m_bush3);




            m_scene2.AddObject(m_end);

            m_scenes[1] = m_scene2;
            */
        }

        /// <summary>
        /// Init level 3
        /// </summary>
        void Level3Init()
        {
        /*
            m_scene3 = new Scene();

            m_player.TriggerLife();

            m_scene3.AddObject(m_grassObj);


            m_player.SetPos2D(m_grid.GetPoint(0.05f, 0.8f));

            m_bush1.SetPos2D(m_grid.GetPoint(0.05f, 0.8f));

            m_bush2.SetPos2D(m_grid.GetPoint(0.40f, 0.5f));
            m_bush3.SetPos2D(m_grid.GetPoint(0.60f, 0.2f));
            m_bush4.SetPos2D(m_grid.GetPoint(0.60f, 0.7f));
            m_bush5.SetPos2D(m_grid.GetPoint(0.3f, 0.3f));




            m_enemy2.SetEnemyState(Enemy.EnemyState.patrolling);
            m_enemy2.SetPos2D(m_grid.GetPoint(0.7f, 0.8f));


            m_enemy3.SetEnemyState(Enemy.EnemyState.patrolling);
            m_enemy3.SetPos2D(m_grid.GetPoint(0.01f, 0.01f));


            m_enemy4.SetEnemyState(Enemy.EnemyState.patrolling);
            m_enemy4.SetPos2D(m_grid.GetPoint(0.2f, 0.1f));


            List<Vector2> patrolPath;

            patrolPath = LoadVec2GridPathFromFile("enemy3.txt");
            m_enemy2.patrolPath = patrolPath;

            patrolPath = LoadVec2GridPathFromFile("enemy4.txt");
            m_enemy3.patrolPath = patrolPath;

            patrolPath = LoadVec2GridPathFromFile("enemy5.txt");
            m_enemy4.patrolPath = patrolPath;

            m_enemy2.StartPatrol(0);
            m_enemy3.StartPatrol(0);
            m_enemy4.StartPatrol(0);






            Shadow playerShadow = new Shadow("pshadow", "shadow", m_player);
            playerShadow.AddAnimSprite("shadow", m_shadow);
            playerShadow.SetAnim("shadow");
            playerShadow.scale = new Vector2(0.11f, 0.11f);
            m_scene3.AddObject(playerShadow);




            Shadow enemy2Shadow = new Shadow("e2shadow", "shadow", m_enemy2);
            enemy2Shadow.AddAnimSprite("shadow", m_shadow);
            enemy2Shadow.SetAnim("shadow");
            enemy2Shadow.scale = new Vector2(0.3f, 0.3f);
            m_scene2.AddObject(enemy2Shadow);




            Shadow enemy3Shadow = new Shadow("e3shadow", "shadow", m_enemy3);
            enemy3Shadow.AddAnimSprite("shadow", m_shadow);
            enemy3Shadow.SetAnim("shadow");
            enemy3Shadow.scale = new Vector2(0.3f, 0.3f);
            m_scene2.AddObject(enemy3Shadow);




            Shadow enemy4Shadow = new Shadow("e4shadow", "shadow", m_enemy4);
            enemy4Shadow.AddAnimSprite("shadow", m_shadow);
            enemy4Shadow.SetAnim("shadow");
            enemy4Shadow.scale = new Vector2(0.3f, 0.3f);

            m_scene3.AddObject(enemy4Shadow);



            m_scene3.AddObject(m_enemy4);

            m_scene3.AddObject(m_enemy2);
            m_scene3.AddObject(m_enemy3);


            m_scene3.AddObject(m_player);

            m_scene3.AddObject(m_end);


            m_scene3.AddObject(m_bush1);
            m_scene3.AddObject(m_bush2);
            m_scene3.AddObject(m_bush3);
            m_scene3.AddObject(m_bush4);
            m_scene3.AddObject(m_bush5);
            {

            }

            m_scenes[2] = m_scene3;
            */
        }

        //Scene m_startScene = new Scene();





        void StartSceneInit()
        {
        /*
            m_startScene = new Scene();
            GameObject2D bg = new GameObject2D("background", "UI");

            bg.SetPos2D(m_grid.GetPoint(0.5f, 0.5f));


            bg.AddAnimSprite("bg", new AnimatedSprite(GetTex("startscreen")));
            bg.SetAnim("bg");

            bg.ScaleToSpriteSize(new Vector2(screenWidth, screenHeight));
            //bg.scale = bg.scale * 0.1f;
            //bg.scale = new Vector2(0.5f,0.5f);


            m_btnEasy.AddAnimSprite("Up", new AnimatedSprite(GetTex("btneasyup")));
            m_btnEasy.AddAnimSprite("Down", new AnimatedSprite(GetTex("btneasydown")));

            m_btnMedium.AddAnimSprite("Up", new AnimatedSprite(GetTex("btnmedup")));
            m_btnMedium.AddAnimSprite("Down", new AnimatedSprite(GetTex("btnmeddown")));

            m_btnHard.AddAnimSprite("Up", new AnimatedSprite(GetTex("btnhardup")));
            m_btnHard.AddAnimSprite("Down", new AnimatedSprite(GetTex("btnharddown")));

            m_btnStart.AddAnimSprite("Up", new AnimatedSprite(GetTex("btnstart")));
            m_btnStart.SetAnim("Up");
            m_btnStart.SetPos2D(m_grid.GetPoint(0.57f, 0.85f));
            //m_btnStart.scale = new Vector2(1f, 1f);

            m_btnEasy.SetAnim("Up");
            m_btnMedium.SetAnim("Down");
            m_btnHard.SetAnim("Up");

            m_btnEasy.SetPos2D(m_grid.GetPoint(0.63f, 0.5f));
            m_btnMedium.SetPos2D(m_grid.GetPoint(0.73f, 0.5f));
            m_btnHard.SetPos2D(m_grid.GetPoint(0.83f, 0.5f));

            m_btnQuit.SetPos2D(m_grid.GetPoint(0.43f, 0.85f));

            m_startScene.AddObject(bg);
            m_startScene.AddObject(m_btnEasy);
            m_startScene.AddObject(m_btnMedium);
            m_startScene.AddObject(m_btnHard);
            m_startScene.AddObject(m_btnStart);
            m_startScene.AddObject(m_btnQuit);

            m_scenes[5] = m_startScene;
            */

        }
    

        void WinSceneInit()
        {
        /*
            m_winScene = new Scene();


            GameObject2D winScreen = new GameObject2D("winScreen", "UI");
            winScreen.AddAnimSprite("default", m_winScreen);
            winScreen.SetAnim("default");
            //winScreen.scale = new Vector2(2, 2);
            winScreen.SetPos2D(m_grid.GetPoint(0.5f, 0.5f));
            m_winScene.AddObject(winScreen);

            m_btnRestart.SetPos2D(m_grid.GetPoint(0.57f, 0.85f));

            m_btnQuit.SetPos2D(m_grid.GetPoint(0.43f, 0.85f));

            m_winScene.AddObject(m_btnRestart);

            m_winScene.AddObject(m_btnQuit);

            m_scenes[4] = m_winScene;

        */

        }

        void NextLevelSceneInit()
        {

        /*
            m_nextLevelScene = new Scene();

            GameObject2D nextScreen = new GameObject2D("nextScreen", "UI");
            nextScreen.AddAnimSprite("default", m_nextLevelScreen);
            nextScreen.SetAnim("default");
            nextScreen.SetPos2D(m_grid.GetPoint(0.5f, 0.5f));
            m_nextLevelScene.AddObject(nextScreen);

            m_btnContinue.SetPos2D(m_grid.GetPoint(0.5f, 0.85f));
            m_btnContinue.AddAnimSprite("Up", new AnimatedSprite(GetTex("btncontinue")));
            m_btnContinue.SetAnim("Up");

            m_nextLevelScene.AddObject(m_btnContinue);

            m_scenes[6] = m_nextLevelScene;



    */

        }

    }
