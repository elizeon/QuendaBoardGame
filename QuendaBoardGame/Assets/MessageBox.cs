using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    Game m_game;
    [SerializeField]
    GameObject m_messageText;
    [SerializeField]
    GameObject m_canvas;

    bool m_canMoveOnResume = true;
    // Use this for initialization
    void Start ()
    {
        m_game = FindObjectOfType<Game>();
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

    public void DisplayMessageBox(string text, bool canMoveOnResume)
    {
        m_game.paused = true;
        m_canvas.SetActive(true);
        m_messageText.GetComponent<Text>().text = text;
        m_canMoveOnResume = canMoveOnResume;
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
            m_game.AllowStartMovement();

        }
    }
    /// <summary>
    /// Closes the message box, not resuming the game.
    /// </summary>
    public void HideMessageBox()
    {
        m_canvas.SetActive(false);

    }
}
