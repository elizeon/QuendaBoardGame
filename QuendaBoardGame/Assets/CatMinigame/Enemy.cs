using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy class, inherits from Gameobject2D, chases Player.
/// </summary>
public class Enemy : GameObject2D
{

    public EnemyState enemyState { get { return m_enemyState; } }
    [SerializeField]
    private GameObject m_patrolPath;
    //public List<Vector2> patrolPath { set { m_patrolPath = value; } get { return m_patrolPath; } }

    private int m_patrolIndex = 0;
    private float m_currentMoveSpeed = 0.1f;
    private float m_defaultMoveSpeed = 0.1f;

    /// <summary>
    /// Set move speed
    /// </summary>
    /// <param name="sp"></param>
    public void SetMoveSpeed(float sp)
    {
        m_defaultMoveSpeed = sp;
        CorrectMovementSpeed();
    }

    public enum EnemyState { patrolling, chasing }
    private EnemyState m_enemyState = EnemyState.chasing;
    /// <summary>
    /// Set enemy state
    /// </summary>
    /// <param name="istate"></param>
    public void SetEnemyState(EnemyState istate)
    {
        //Console.WriteLine("Enemy state changed to " + istate);
        m_enemyState = istate;
        CorrectMovementSpeed();
    }

    private void CorrectMovementSpeed()
    {
        if (m_enemyState == EnemyState.patrolling)
        {
            m_currentMoveSpeed = m_defaultMoveSpeed;
        }
        if (m_enemyState == EnemyState.chasing)
        {
            m_currentMoveSpeed = m_defaultMoveSpeed * 3;
        }
    }

    float m_chaseSpeedMultiplier = 3;
    
    /// <summary>
    /// Sets the enemy
    /// </summary>
    /// <param name="newid"></param>
    /// <param name="newtype"></param>
    /// <param name="newhp"></param>
    /// <param name="moveSpeed"></param>
    /// <param name="chaseSpeedMultiplier"></param>
    public void Set(string newid, string newtype, float newhp, float moveSpeed, float chaseSpeedMultiplier, EnemyState startingState)
    {
        id = newid;
        hp = newhp;
        type = newtype;
        viewDist = 100;
        viewAngle = 20;
        hiddenViewDist = 75;
        closeViewDist = 100;
        m_defaultMoveSpeed = moveSpeed;
        m_chaseSpeedMultiplier = chaseSpeedMultiplier;
        m_enemyState = startingState;
    }
    
    
    public override void Update()
    {

        if (m_enemyState == EnemyState.patrolling)
        {
        Debug.Log("Patrolling");

        Player player = catGame.player;
        bool chase = false;

        if (player.gameObject.activeSelf && _2DUtil.CheckSphereCollision(pos2D, viewDist, player.pos2D, player.boundingBox.Width))
        {
            float rotToPlayer = _2DUtil.LookAt(pos2D, player.pos2D);



            double ydir = Mathf.Cos(rotation);
            double xdir = Mathf.Sin(rotation);


            if (!player.hiding &&// If the player is not hiding
                                    // Is the player within the enemy's view angle?
                (Utility.WithinAngle(rotToPlayer, rotation, viewAngle))) //||
                    // Is the player in the close view distance sphere?
                    //_2DUtil.CheckSphereCollision(pos2D, closeViewDist, player.pos2D, player.boundingBox.Width)))
            {
                chase = true;
            }
            else
            {
                // If the player is within the cat's hidden sight
                //if (_2DUtil.CheckSphereCollision(pos2D, hiddenViewDist, player.pos2D, player.boundingBox.Width))
                {
                    //chase = true;
                }
            }


        }

        if (chase)
        {
            SetEnemyState(EnemyState.chasing);

        }


        GameObject patrolNext = m_patrolPath.transform.GetChild(m_patrolIndex).gameObject;
        Vector2 patrolLoc = patrolNext.transform.position;


        if (!_2DUtil.IsAt(this.pos2D, patrolLoc))
            {
                Debug.Log("Moving towards next patrol point. ");
                
                _2DUtil.PrintVec(patrolLoc);

                //m_currentMoveSpeed = 1000;
                SetPos2D( Vector2.MoveTowards(pos2D, patrolLoc, Time.fixedDeltaTime * m_currentMoveSpeed));
                //_2DUtil.MoveTowards(this, patrolPath[m_patrolIndex], Time.fixedDeltaTime * m_currentMoveSpeed);
                //Console.WriteLine(pos2D.X + ", " + pos2D.Y + " / " + patrolPath[m_patrolIndex].X + ", " + patrolPath[m_patrolIndex].Y);

                if (pos2D == patrolLoc)
                {
                    if (m_patrolIndex < m_patrolPath.transform.childCount - 1)
                    {
                        Debug.Log("Reached next patrol point.");
                        m_patrolIndex += 1;
                        StartPatrol(m_patrolIndex);

                    }
                    else
                    {
                        m_patrolIndex = 0;
                        StartPatrol(m_patrolIndex);
                        Debug.Log("Reached end, restarting patrol.");
                    }

                }

            }


        }

        else
        {
            if (m_enemyState == EnemyState.chasing)
            {
            Debug.Log("Chasing");

                Vector3 angle = transform.position - catGame.player.transform.position;
                float ang = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
                ang += 90;
                //transform.LookAt(target, Vector3.forward);
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, ang));
                
                direction = catGame.player.pos2D - this.pos2D;
                direction.Normalize();
                SetPos2D( Vector3.MoveTowards(pos2D, catGame.player.pos2D, Time.fixedDeltaTime * m_currentMoveSpeed));
                //_2DUtil.MoveTowards(this, catGame.player.pos2D, Time.fixedDeltaTime * m_currentMoveSpeed);
            }
        }

    }

        /// <summary>
        /// The view distance in which the enemy will see the player even if they are not in their view angle.
        /// Will have no effect if greater than viewDist.
        /// </summary>
        public float closeViewDist { get; set; }

        /// <summary>
        /// The view distance in which the enemy will see the player even if they are hidden.
        /// Will have no effect if greater than viewDist.
        /// </summary>
        public float hiddenViewDist { get; set; }

    /// <summary>
    /// Starts the patrol.
    /// </summary>
    /// <param name="newPatrolIndex">Index to start patrol at.</param>
    public void StartPatrol(int newPatrolIndex)
    {
        Vector2 source = this.pos2D;
        Vector2 dest = m_patrolPath.transform.GetChild(newPatrolIndex).position;



        Vector3 angle = transform.position - m_patrolPath.transform.GetChild(newPatrolIndex).position;
        float ang = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        ang += 90;
        //transform.LookAt(target, Vector3.forward);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, ang));



        rotation = _2DUtil.LookAt(source, dest);
        direction = dest - this.pos2D;
        m_patrolIndex = newPatrolIndex;



    }

        public float viewDist { get; set; }

        public float viewAngle { get; set; }

    }
