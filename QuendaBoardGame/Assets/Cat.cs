using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Class for 3D patrolling object
 * Written by Elizabeth Haynes
 * 
 * */

public class Cat : MonoBehaviour {


    [SerializeField]
    GameObject m_patrolPath;
	// Use this for initialization
	void Start ()
    {
        this.transform.position = m_patrolPath.transform.GetChild(1).position;


    }

    float m_currentMoveSpeed = 1f;
    int m_patrolIndex = 1;
    // Update is called once per frame
	void Update ()

    {

        GameObject patrolLoc = m_patrolPath.transform.GetChild(m_patrolIndex).gameObject;

        
        if (this.transform.position != patrolLoc.transform.position)
        {
            //Debug.Log("Moving towards next patrol point. ");

            //m_currentMoveSpeed = 1000;
            transform.position = Vector3.MoveTowards(transform.position, patrolLoc.transform.position, Time.deltaTime * m_currentMoveSpeed);
            //_2DUtil.MoveTowards(this, patrolPath[m_patrolIndex], Time.fixedDeltaTime * m_currentMoveSpeed);
            //Console.WriteLine(pos2D.X + ", " + pos2D.Y + " / " + patrolPath[m_patrolIndex].X + ", " + patrolPath[m_patrolIndex].Y);
            

        }
        else
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

    public void StartPatrol(int newPatrolIndex)
    {
        Vector3 source = this.transform.position;
        Vector3 dest = m_patrolPath.transform.GetChild(newPatrolIndex).position;

        this.transform.LookAt(dest);
        

        m_patrolIndex = newPatrolIndex;

    }
}