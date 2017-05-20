using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player class
/// </summary>
public class Player : GameObject2D
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            catGame.Lose();
        }

        if(col.CompareTag("HideLoc"))
        {
            hiding = true;
        }

        if(col.CompareTag("Exit"))
        {
            catGame.Win();
        }
    }

    public void SetMoveSpeed(float sp)
    {
        m_moveSpeed = sp;
    }

    void OnTriggerExit2D(Collider2D col)
    {

        Debug.Log("Player exit trigger " + col.gameObject.tag);
        if (col.CompareTag("HideLoc"))
        {
            hiding = false;
        }
    }
    
    private bool m_hiding = false;
    public bool hiding { get { return m_hiding; } set { m_hiding = value; } }
    private float m_moveSpeed = 1f;

    private Vector2 m_targetLoc;


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
    
    bool m_playerMoving = false;
        
    public void Awake()
    {
        hiding = true;
    }

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
            
        /*if (Input.GetMouseButtonDown(0))
        {
                //m_targetLoc =  new Vector2(mouse.Position.X,mouse.Position.Y);
                m_playerMoving = true;
                m_targetLoc = catGame.gameGrid.GetPoint(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
                Vector2 source = this.pos2D;
                Vector2 dest = m_targetLoc;


                rotation = _2DUtil.LookAt(source, dest);
                //
                direction = dest - this.pos2D;
              


        }*/
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
        Debug.Log("Hiding: " + hiding);


    }
}