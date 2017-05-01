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
    List<TextAsset> m_enemyPaths;
    [SerializeField]
    GameObject m_grassObj;
    [SerializeField]
    GameObject m_bush;
    [SerializeField]
    GameObject m_player;
    [SerializeField]
    GameObject m_enemy;

    Player m_playerp;
    public Player player { get { return m_playerp; } }



    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(m_camera);
        DontDestroyOnLoad(m_player);
        DontDestroyOnLoad(m_enemy);
        DontDestroyOnLoad(m_bush);

        m_playerp = m_player.GetComponent<Player>();
        m_playerp.Set("player", "player", 10);

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

        m_grid = new GameGrid(10, 10, 0.1f);


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

                // Write points to file
                if (Input.GetKeyDown(KeyCode.W))
                {
                    float t = Time.timeSinceLevelLoad;

                    WriteVec2sToFile(m_debugPath, "Path " +t+ ".txt");

                    EnterScene(2);
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


    public void ReturnToGame()
    {
        // todo add level to file->build settings
        Application.LoadLevel(0);
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
        public static int difficulty = 1;


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

        HideLoc m_bush1 = new HideLoc("bush1", "hideloc", 0);
        HideLoc m_bush2 = new HideLoc("bush2", "hideloc", 0);
        HideLoc m_bush3 = new HideLoc("bush3", "hideloc", 0);
        HideLoc m_bush4 = new HideLoc("bush4", "hideloc", 0);
        HideLoc m_bush5 = new HideLoc("bush5", "hideloc", 0);
        Enemy m_enemy1 = new Enemy("enemy1", "enemy", 0);
        Enemy m_enemy2 = new Enemy("enemy1", "enemy", 0);
        Enemy m_enemy3 = new Enemy("enemy1", "enemy", 0);
        Enemy m_enemy4 = new Enemy("enemy1", "enemy", 0);
        GameObject2D m_end = new GameObject2D("end", "end");



        // UI Buttons
        GameObject2D m_btnEasy = new GameObject2D("buttoneasy", "button");
        GameObject2D m_btnMedium = new GameObject2D("buttonmed", "button");
        GameObject2D m_btnHard = new GameObject2D("buttonhard", "button");
        GameObject2D m_btnStart = new GameObject2D("buttonstart", "button");
        GameObject2D m_btnContinue = new GameObject2D("buttonstart", "button");
        GameObject2D m_btnQuit = new GameObject2D("buttonquit", "button");
        GameObject2D m_btnRestart = new GameObject2D("buttonrestart", "button");

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

        /// Collision event. Pass the level and move to scene 6 (level passed)
        /// If levels passed is more then m_totalLevels Win is triggered.
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="thisobj">this object</param>
        /// <param name="other">other object</param>
        void PassLevel(GameObject2D thisobj, GameObject2D other)
        {
            m_levelsPassed += 1;
            if (m_levelsPassed >= m_totalLevels)
            {
                Win(thisobj, other);
            }
            else
            {
                EnterScene(6);
            }
        }


        /// <summary>
        /// Collision event. Triggers game win.
        /// </summary>
        /// <param name="gameTime">game time </param>
        /// <param name="thisobj">this object</param>
        /// <param name="other">other object</param>
        void Win(GameObject2D thisobj, GameObject2D other)
        {
            EnterScene(4);
        }

        /// <summary>
        /// Collision event with Enemy. The enemy resumes patrolling and the scene switches to 3 (Lose).
        /// </summary>
        /// <param name="gameTime">game time </param>
        /// <param name="thisobj">this object</param>
        /// <param name="other">other object</param>
        void OtherTakesDamageStop(Enemy thisobj, GameObject2D other)
        {

            //Console.WriteLine("Collision event triggered: Enemy hit player");
            thisobj.SetEnemyState(Enemy.EnemyState.patrolling);
            EnterScene(3);
        }

    
    
        /// <summary>
        /// Load and start the scene of chosen index.
        /// </summary>
        /// <param name="scn">Index of scene in m_scenes.</param>
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
                case 3:
                    LoseSceneInit();
                    break;
                case 4:
                    WinSceneInit();
                    break;
                case 5:
                    StartSceneInit();
                    break;
                case 6:
                    NextLevelSceneInit();
                    break;
            }

            currentScene = scn;
        }


        void StartGame()
        {
            m_currentLevel = 0;
            EnterScene(0);
        }

        //AnimatedSprite m_shadow;

        /// <summary>
        /// Initialise global objects before starting scenes.
        /// </summary>
        void GlobalObjectsInit()
        {
        /*
            m_quendaSpritesUD = GetTex("quendaspritesheet");
            m_playerRunUp = new AnimatedSprite(m_quendaSpritesUD, 1, 5);
            m_playerIdle = new AnimatedSprite(m_quendaSpritesUD, 1, 5, 4, 5);
            m_bush2D = GetTex("bush");
            Texture2D texcat = GetTex("cat_fluffy");
            m_enemyWalk = new AnimatedSprite(texcat, 8, 12, 48, 51);
            m_enemyWalk.FlipVertical(true);
            m_enemyWalk.playSpeed = 0.5f;



            m_player.scale = new Vector2(1f, 1f);

            m_player.AddAnimSprite("runUp", m_playerRunUp);
            m_player.AddAnimSprite("idle", m_playerIdle);
            m_player.SetAnim("idle");
            m_player.defaultAnim = "idle";

            //m_enemy1.collisions = false;



            m_enemy1.scale = new Vector2(1f, 1f);
            m_enemy1.AddAnimSprite("walk", m_enemyWalk);
            m_enemy1.SetAnim("walk");

            m_enemy2.scale = new Vector2(1f, 1f);
            m_enemy2.AddAnimSprite("walk", m_enemyWalk);
            m_enemy2.SetAnim("walk");


            m_enemy3.scale = new Vector2(1f, 1f);
            m_enemy3.AddAnimSprite("walk", m_enemyWalk);
            m_enemy3.SetAnim("walk");


            m_enemy4.scale = new Vector2(1f, 1f);
            m_enemy4.AddAnimSprite("walk", m_enemyWalk);
            m_enemy4.SetAnim("walk");

            m_winScreen = new AnimatedSprite(GetTex("winscreen"), 1, 1);
            m_loseScreen = new AnimatedSprite(GetTex("losescreen"), 1, 1);


            AnimatedSprite bush = new AnimatedSprite(m_bush2D);
            m_bush1.AddAnimSprite("idle", bush);
            m_bush1.scale = new Vector2(1f, 1f);
            m_bush1.SetAnim("idle");


            m_bush2.AddAnimSprite("idle", bush);
            m_bush2.scale = new Vector2(1f, 1f);
            m_bush2.SetAnim("idle");

            m_bush3 = new HideLoc("bush3", m_bush1);
            m_bush3.SetAnim("idle");

            m_bush4 = new HideLoc("bush4", m_bush1);
            m_bush4.SetAnim("idle");

            m_bush5 = new HideLoc("bush5", m_bush1);
            m_bush5.SetAnim("idle");

            // Collision triggers



            m_endspr = new AnimatedSprite(GetTex("exit"));

            m_grass = new AnimatedSprite(GetTex("grass"));
            m_grassObj = new GameObject2D("grass", "grass");
            m_grassObj.AddAnimSprite("grassspr", m_grass);
            m_grassObj.SetAnim("grassspr");
            m_grassObj.SetPos2D(m_grid.GetPoint(0.5f, 0.5f));

            //m_grassObj.scale = new Vector2();//2*screenWidth / (m_grass.texture.Width),2* screenHeight / (m_grass.texture.Height));
            m_grassObj.ScaleToSpriteSize(new Vector2(screenWidth, screenHeight));
            //m_grassObj.scale = new Vector2(3, 3);
            //m_grassObj.scale *= 2;


            m_end.SetPos2D(m_grid.GetPoint(0.99f, 0.5f));
            Vector2 pointa = m_grid.GetPoint(0.9f, 0.01f);
            Vector2 pointb = m_grid.GetPoint(0.99f, 0.99f);
            m_end.SetCustomBoundingBox(new Rectangle((int)pointa.X, (int)pointa.Y, (int)(pointb.X - pointa.X), (int)(pointb.Y - pointa.Y)));
            m_end.AddAnimSprite("default", m_endspr);
            m_end.SetAnim("default");
            m_end.scale = new Vector2(3f, 1f);

            m_nextLevelScreen = new AnimatedSprite((GetTex("completearea")));


            m_btnQuit.AddAnimSprite("Up", new AnimatedSprite(GetTex("btnquit")));
            m_btnQuit.SetAnim("Up");

            m_btnContinue.AddAnimSprite("Up", new AnimatedSprite(GetTex("btncontinue")));
            m_btnContinue.SetAnim("Up");

            m_btnRestart.AddAnimSprite("Up", new AnimatedSprite(GetTex("btnrestart")));
            m_btnRestart.SetAnim("Up");

            m_shadow = new AnimatedSprite(GetTex("dropshadow"));


            switch (difficulty)
            {
                case 0:
                    m_enemy1.viewDist = 100;
                    m_enemy1.SetMoveSpeed(m_easySpeed);
                    m_enemy2.viewDist = 100;
                    m_enemy2.SetMoveSpeed(m_easySpeed);
                    m_enemy3.viewDist = 100;
                    m_enemy3.SetMoveSpeed(m_easySpeed);
                    m_enemy4.viewDist = 100;
                    m_enemy4.SetMoveSpeed(m_easySpeed);
                    break;

                case 1:
                    m_enemy1.viewDist = Utility.Max(Game1.screenWidth, Game1.screenHeight);
                    m_enemy1.SetMoveSpeed(m_normalSpeed);
                    m_enemy2.viewDist = Utility.Max(Game1.screenWidth, Game1.screenHeight);
                    m_enemy2.SetMoveSpeed(m_normalSpeed);
                    m_enemy3.viewDist = Utility.Max(Game1.screenWidth, Game1.screenHeight);
                    m_enemy3.SetMoveSpeed(m_normalSpeed);
                    m_enemy4.viewDist = Utility.Max(Game1.screenWidth, Game1.screenHeight);
                    m_enemy4.SetMoveSpeed(m_normalSpeed);

                    break;

                case 2:
                    m_enemy1.viewDist = Utility.Max(Game1.screenWidth, Game1.screenHeight);
                    m_enemy1.SetMoveSpeed(m_hardSpeed);
                    m_enemy2.viewDist = Utility.Max(Game1.screenWidth, Game1.screenHeight);
                    m_enemy2.SetMoveSpeed(m_hardSpeed);
                    m_enemy3.viewDist = Utility.Max(Game1.screenWidth, Game1.screenHeight);
                    m_enemy3.SetMoveSpeed(m_hardSpeed);
                    m_enemy4.viewDist = Utility.Max(Game1.screenWidth, Game1.screenHeight);
                    m_enemy4.SetMoveSpeed(m_hardSpeed);

                    break;
                default:
                    Console.WriteLine("No difficulty level set.");
                    Console.ReadKey();
                    break;
            }
        */
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

    [SerializeField]
    GameObject m_camera;


    IEnumerator LoadLevel1()
    {

        yield return new WaitForEndOfFrame();
        Scene nextScene = SceneManager.GetSceneByBuildIndex(2);

        SceneManager.UnloadSceneAsync(1);


        SceneManager.LoadScene(nextScene.buildIndex, LoadSceneMode.Additive);


        SceneManager.SetActiveScene(nextScene);

    }
    /// <summary>
    /// Init level 1
    /// </summary>
    void Level1Init()
        {

        LoadLevel1();


        //SceneManager.MoveGameObjectToScene(gameObject, nextScene);

        //GameObject[] goArray = SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();

        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(m_camera, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(m_enemy, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(m_player, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(m_bush, SceneManager.GetActiveScene());
        

        m_playerp.TriggerLife();
        m_playerp.SetPos2D(new Vector2(0, 0));
        //m_playerp.TriggerLife();

        HideLoc m_bush1 = Instantiate(m_bush).GetComponent<HideLoc>();
        m_bush1.gameObject.SetActive(true);
        HideLoc m_bush2 = Instantiate(m_bush).GetComponent<HideLoc>();
        m_bush2.gameObject.SetActive(true);

        HideLoc m_bush3 = Instantiate(m_bush).GetComponent<HideLoc>();
        m_bush3.gameObject.SetActive(true);

        HideLoc m_bush4 = Instantiate(m_bush).GetComponent<HideLoc>();
        m_bush4.gameObject.SetActive(true);



        m_bush1.SetPos2D(m_grid.GetPoint(0.1f, 0.7f));
        m_bush2.SetPos2D(m_grid.GetPoint(0.3f, 0.3f));
        m_bush3.SetPos2D(m_grid.GetPoint(0.6f, 0.6f));
        m_bush4.SetPos2D(m_grid.GetPoint(0.8f, 0.4f));


        m_playerp.SetPos2D(m_grid.GetPoint(0.1f, 0.7f));
        m_playerp.SetPos2D(new Vector2(0, 10));


        

        GameObject m_player1 = Instantiate(m_player);
        Player m_player1p = m_player1.GetComponent<Player>();
        m_player1.SetActive(true);

        GameObject m_enemy1 = Instantiate(m_enemy);
        Enemy m_enemy1e = m_enemy1.GetComponent<Enemy>();

        List<Vector2> patrolPath = //new List<Vector2>(); patrolPath.Add(m_grid.GetPoint(0.9f, 0.9f));
        LoadVec2GridPathFromFile(m_enemyPaths[0]);
        m_enemy1e.patrolPath = patrolPath;
        m_enemy1e.SetEnemyState(Enemy.EnemyState.patrolling);
        m_enemy1e.StartPatrol(1);

        m_enemy1e.SetPos2D(m_grid.GetPoint(0.3f, 0.2f));

        m_enemy1e.SetPos2D(new Vector2(0, 0));
        m_enemy1.SetActive(true);




        

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

        void LoseSceneInit()
        {
        /*
            m_loseScene = new Scene();
            GameObject2D loseScreen = new GameObject2D("loseScreen", "UI");
            loseScreen.AddAnimSprite("default", m_loseScreen);
            loseScreen.SetAnim("default");
            loseScreen.SetPos2D(m_grid.GetPoint(0.5f, 0.5f));
            m_loseScene.AddObject(loseScreen);


            m_btnRestart.SetPos2D(m_grid.GetPoint(0.57f, 0.85f));

            m_btnQuit.SetPos2D(m_grid.GetPoint(0.43f, 0.85f));

            m_loseScene.AddObject(m_btnRestart);

            m_loseScene.AddObject(m_btnQuit);

            m_scenes[3] = m_loseScene;
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
