using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script for unity UI screen that presents a Quiz to the player.
 * Written by Elizabeth Haynes
 * */
public class QuizScreen : MonoBehaviour
{

    [SerializeField]
    GameObject m_button1;

    [SerializeField]
    GameObject m_background;

    [SerializeField]
    GameObject m_button2;

    [SerializeField]
    GameObject m_button3;

    [SerializeField]
    GameObject m_button4;

    [SerializeField]
    GameObject m_answerText1;

    [SerializeField]
    GameObject m_answerText2;

    [SerializeField]
    GameObject m_answerText3;

    [SerializeField]
    GameObject m_answerText4;

    [SerializeField]
    GameObject m_hintText;

    [SerializeField]
    GameObject m_questionText;

    private Quiz m_quiz;

    public void SetQuiz(Quiz val)
    {
        m_quiz = val;
    }

    [SerializeField]
    GameObject m_canvas;

    int m_answer;

    Game m_game;

    // Use this for initialization
    void Start ()
    {
        m_game = FindObjectOfType<Game>();

        ToggleElements(false);
	}

    /// <summary>
    /// Toggle whether quiz elements are active (true) or disabled (false).
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleElements(bool toggle)
    {
        m_background.SetActive(toggle);
        m_questionText.SetActive(toggle);
        m_hintText.SetActive(toggle);


        if (toggle)
        {
            m_game.DisallowStartMovement();
        }
        else
        {

            m_button1.SetActive(toggle);
            m_button2.SetActive(toggle);
            m_button3.SetActive(toggle);
            m_button4.SetActive(toggle);
        }
       
        
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
    public void SetQuestion(string text)
    {
        m_questionText.GetComponent<Text>().text = text;
    }

    public void SetQuizNode(int index, string text)
    {
        switch(index)
        {
            case 1:
                m_button1.SetActive(true);

                m_answerText1.GetComponent<Text>().text = text;
                break;
            case 2:
                m_button2.SetActive(true);

                m_answerText2.GetComponent<Text>().text = text;
                break;
            case 3:
                m_button3.SetActive(true);

                m_answerText3.GetComponent<Text>().text = text;
                break;
            case 4:
                m_button4.SetActive(true);

                m_answerText4.GetComponent<Text>().text = text;
                break;
        }
    }

    /// <summary>
    /// Set the correct answer to the quiz
    /// </summary>
    /// <param name="ans"></param>
    public void SetAnswer(int ans)
    {
        m_answer = ans;
    }

    string m_incorrectMessage;
    string m_correctMessage;

    /// <summary>
    /// Sets the correct message.
    /// </summary>
    /// <param name="message">The message.</param>
    public void SetCorrectMessage(string message)
    {
        m_correctMessage = message;
    }

    /// <summary>
    /// Sets the hint.
    /// </summary>
    /// <param name="hint">The hint.</param>
    public void SetHint(string hint)
    {
        m_hintText.GetComponent<Text>().text = hint;
    }
    /// <summary>
    /// Sets the incorrect message.
    /// </summary>
    /// <param name="message">The message.</param>
    public void SetIncorrectMessage(string message)
    {
        m_incorrectMessage = message;
    }

    /// <summary>
    /// Begin the quiz. Expects the quiz has already been set up with SetQuestion, SetAnswer, SetQuizNode, SetCorrectMessage and SetIncorrectMessage.
    /// </summary>
    public void StartQuiz()
    {
        //m_canvas.SetActive(true);

        ToggleElements(true);

    }

    /// <summary>
    /// End the quiz and resume the game.
    /// </summary>
    public void EndQuiz(int i)
    {
        string message;

        if (i == m_answer)
        {
            message= m_correctMessage + " Move forward 3 spaces.";
            m_game.AllowStartMovement();
            m_game.MoveOnPath(3);
            m_game.AddResult(m_quiz.name, true);
        }
        else
        {
            message =m_incorrectMessage + " Move back 3 spaces.";
            m_game.AllowStartMovement();
            m_game.MoveOnPath(-3);
            m_game.AddResult(m_quiz.name, false);
        }
        //m_canvas.SetActive(false);
        ToggleElements(false);
        m_game.messageBox.DisplayMessageBox(message);

    }

    /// <summary>
    /// Have the player pick their answer
    /// </summary>
    /// <param name="i"></param>
    public void ChooseAnswer(int i)
    {

        EndQuiz(i);

    }
}
