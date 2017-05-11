﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy class, inherits from Gameobject2D, chases Player.
/// </summary>
public class Enemy : GameObject2D
{

    public EnemyState enemyState { get { return m_enemyState; } }

    private List<Vector2> m_patrolPath = new List<Vector2>();
    public List<Vector2> patrolPath { set { m_patrolPath = value; } get { return m_patrolPath; } }

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
        if (istate == EnemyState.patrolling)
        {
            m_currentMoveSpeed = m_defaultMoveSpeed;
        }
        if (istate == EnemyState.chasing)
        {
            m_currentMoveSpeed = m_chaseSpeed;
        }
    }

    float m_chaseSpeed = 1;

    //Table<GameObject2D, Action<GameTime, Enemy, GameObject2D>> collisionTriggersWithThisType = new Table<GameObject2D, Action<GameTime, Enemy, GameObject2D>>(COLTRIGGERSIZE);

        /*
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="newid">id</param>
    /// <param name="newtype">type</param>
    /// <param name="newhp">hitpoints, 0 for invincible</param>
    public Enemy(string newid, string newtype, float newhp, float moveSpeed, float chaseSpeed) : base(newid, newtype, newhp)
    {
        viewDist = 100;
        viewAngle = 20;
        hiddenViewDist = 75;
        closeViewDist = 100;
        m_defaultMoveSpeed = moveSpeed;
        m_chaseSpeed = chaseSpeed;
    }
    */

    public void Set(string newid, string newtype, float newhp, float moveSpeed, float chaseSpeed)
    {
        id = newid;
        hp = newhp;
        type = newtype;
        viewDist = 100;
        viewAngle = 20;
        hiddenViewDist = 75;
        closeViewDist = 100;
        m_defaultMoveSpeed = moveSpeed;
        m_chaseSpeed = chaseSpeed;
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
                (Utility.WithinAngle(rotToPlayer, rotation, viewAngle) ||
                    // Is the player in the close view distance sphere?
                    _2DUtil.CheckSphereCollision(pos2D, closeViewDist, player.pos2D, player.boundingBox.Width)))
            {
                chase = true;
            }
            else
            {
                // If the player is within the cat's hidden sight
                if (_2DUtil.CheckSphereCollision(pos2D, hiddenViewDist, player.pos2D, player.boundingBox.Width))
                {
                    chase = true;
                }
            }


        }

        if (chase)
        {
            SetEnemyState(EnemyState.chasing);

        }
            
        if (!_2DUtil.IsAt(this.pos2D, patrolPath[m_patrolIndex]))
            {
                Debug.Log("Moving towards next patrol point. ");
                _2DUtil.PrintVec(patrolPath[m_patrolIndex]);
                _2DUtil.MoveTowards(this, patrolPath[m_patrolIndex], Time.fixedDeltaTime * m_currentMoveSpeed);
                //Console.WriteLine(pos2D.X + ", " + pos2D.Y + " / " + patrolPath[m_patrolIndex].X + ", " + patrolPath[m_patrolIndex].Y);

                if (_2DUtil.IsAt(pos2D, patrolPath[m_patrolIndex]))
                {
                    if (m_patrolIndex < m_patrolPath.Count - 1)
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
                rotation = _2DUtil.LookAt(this.pos2D, catGame.player.pos2D);
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

        public void StartPatrol(int newPatrolIndex)
        {
            Vector2 source = this.pos2D;
            Vector2 dest = m_patrolPath[newPatrolIndex];


            rotation = _2DUtil.LookAt(source, dest);
            direction = dest - this.pos2D;
            m_patrolIndex = newPatrolIndex;

        }

        public float viewDist { get; set; }

        public float viewAngle { get; set; }

    }
