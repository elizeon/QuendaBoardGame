using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class for in-game message box
 * 
 * Written by Elizabeth Haynes 
 * */
public class MessageBox : MonoBehaviour
{
    Game m_game;
    [SerializeField]
    GameObject m_messageText;
    [SerializeField]
    GameObject m_canvas;

    bool m_canMoveOnResume = true;

    public bool canMoveOnResume { get { return m_canMoveOnResume; } set { m_canMoveOnResume = value; } }
    // Use this for initialization
    void Awake ()
    {
        m_game = FindObjectOfType<Game>();
	}

    void Start()
    {
        HideMessageBox();

    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void DisplayMessageBox(string text)
    {
        m_game.paused = true;
        m_canvas.SetActive(true);
        m_messageText.GetComponent<Text>().text = text;
        m_canMoveOnResume = true;
    }

    public void DisplayMessageBox(string text, bool moveOnResume)
    {
        m_game.paused = true;
        m_canvas.SetActive(true);
        m_messageText.GetComponent<Text>().text = text;
        m_canMoveOnResume = moveOnResume;
    }

    /// <summary>
    /// Closes the message box and resumes player movement.
    /// </summary>
    public void CloseMessageBox()
    {
        m_game.paused = false;
        HideMessageBox();
        if(m_canMoveOnResume)
        {
            Debug.Log("Closing message box allowed resume start movement.");
            //m_game.AllowStartMovement();

        }
    }
    /// <summary>
    /// Closes the message box, not resuming the game.
    /// </summary>
    public void HideMessageBox()
    {
        m_canvas.SetActive(false);

    }

    public bool Active()
    {
        if(m_canvas.activeSelf)
        {
            return true;
        }
        return false;
    }
}
