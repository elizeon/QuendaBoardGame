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
        m_canvas.SetActive(true);
        m_messageText.GetComponent<Text>().text = text;
    }

    /// <summary>
    /// Closes the message box and resumes player movement.
    /// </summary>
    public void CloseMessageBox()
    {
        HideMessageBox();
        m_game.AllowMovement();
    }
    /// <summary>
    /// Closes the message box, not resuming the game.
    /// </summary>
    public void HideMessageBox()
    {
        m_canvas.SetActive(false);

    }
}
