using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Board game level with ID and list of paths
 * Written by Elizabeth Haynes
 * */

public class Level : MonoBehaviour
{

    [SerializeField]
    private int m_id;
    public int id { get { return m_id; } set { m_id = value; } }


    [SerializeField]
    private List<GameObject> m_allPaths = new List<GameObject>();


    /// <summary>
    /// A list of all paths in the game.
    /// </summary>
    public List<GameObject> allPaths { get { return m_allPaths; } set { m_allPaths = value; } }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
