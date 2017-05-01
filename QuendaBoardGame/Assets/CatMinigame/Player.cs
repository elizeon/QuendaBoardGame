using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player class
/// </summary>
public class Player : GameObject2D
{
	// Use this for initialization
	void Start ()
    {
	}
        private bool m_hiding = false;
        public bool hiding { get { return m_hiding; } set { m_hiding = value; } }
        private float m_moveSpeed = 0.3f;

        private Vector2 m_targetLoc;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="newid"></param>
        /// <param name="newtype"></param>
        /// <param name="newhp"></param>
        public Player(string newid, string newtype, float newhp) : base(newid, newtype, newhp)
        {

        }

        /// <summary>
        /// True if initial position of player has not been set.
        /// </summary>
        bool m_initPos = true;
        public override void SetPos2D(Vector2 newpos)
        {
            base.SetPos2D(newpos);
            if (m_initPos)
            {
                m_targetLoc = pos2D;
                m_initPos = false;

            }
        }

        /// <summary>
        /// Render
        /// </summary>
        /// <param name="sprBatch"></param>
        /*
        public override void Render(SpriteBatch sprBatch)
        {
            base.Render(sprBatch);
            //Render2D.Instance.DrawRectangle(sprBatch, new Rectangle((int)pos2D.X, (int)pos2D.Y, 10, 10), Color.White);
        }


        Table<string, Action<GameTime, Player, GameObject2D>> collisionTriggersWithThisType = new Table<string, Action<GameTime, Player, GameObject2D>>(COLTRIGGERSIZE);

        /// <summary>
        /// Add collision trigger for player
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="method"></param>
        public void AddCollisionTrigger(string objType, Action<GameTime, Player, GameObject2D> method)
        {
            collisionTriggersWithThisType.Add(objType, method);
        }
        /// <summary>
        /// Process player collision event
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="otherObj"></param>
        public void ProcessCollisionEventPlayer(GameTime gameTime, GameObject2D otherObj)
        {
            if (collisionTriggersWithThisType.ContainsKey(otherObj.type))
            {
                collisionTriggersWithThisType.Get(otherObj.type).DynamicInvoke(gameTime, this, otherObj);
            }
        }
        */

        bool m_playerMoving = false;
        /// <summary>
        /// Bring to life
        /// </summary>
        public override void TriggerLife()
        {
            base.TriggerLife();
            m_initPos = true;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update()
        {
            // Hiding
            List<GameObject2D> toReturn = new List<GameObject2D>();
            bool tohide = false;

        /*
            for (int i = 0; i < collisionEvents.Count; i++)
            {
                var val = collisionEvents[i];

                if (val.type != "end")
                {
                    ProcessCollisionEventPlayer(gameTime, val);

                }

                switch (val.type)
                {
                    case "hideloc":
                        {
                            tohide = true;
                            break;
                        }
                }

            }

            if (tohide)
            {
                hiding = true;
            }
            else
            {
                hiding = false;
            }

            for (int i = 0; i < collisionEvents.Count; i++)
            {

            }
            */

            base.Update();

            // Player input
            
            if (Input.GetMouseButtonDown(0))
            {
                    //m_targetLoc =  new Vector2(mouse.Position.X,mouse.Position.Y);
                    m_playerMoving = true;
                    m_targetLoc = catGame.gameGrid.GetPoint(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
                    Vector2 source = this.pos2D;
                    Vector2 dest = m_targetLoc;


                    rotation = _2DUtil.LookAt(source, dest);
                    //
                    direction = dest - this.pos2D;
              


            }
            // Player movement

            if (m_playerMoving && !_2DUtil.IsAt(this.pos2D, m_targetLoc))
            {
                _2DUtil.MoveTowards(this, m_targetLoc, Time.fixedDeltaTime * m_moveSpeed);
            //Console.WriteLine(pos2D.X + ", " + pos2D.Y + " / " + patrolPath[m_patrolIndex].X + ", " + patrolPath[m_patrolIndex].Y);

            // todo set moving anim
            /*
            if (animSprite != animSprites["runUp"])
                {
                    animSprite = animSprites["runUp"];
                }
            */
        }
        else
            {
                m_playerMoving = false;
            }

            if (!m_playerMoving)
            {

                // todo set static anim
                /*
                if (animSprite != animSprites["idle"])
                {
                    animSprite = animSprites["idle"];
                }
                */
            }
            // Player combat




            //Console.WriteLine("Hiding: " + hiding);



            //Console.WriteLine("Player location at " + pos2D.X, ", " + pos2D.Y);


        }
    }