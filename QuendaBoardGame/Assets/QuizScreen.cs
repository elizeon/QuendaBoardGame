using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizScreen : MonoBehaviour
{

    [SerializeField]
    GameObject m_button1;

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
    GameObject m_questionText;

    [SerializeField]
    GameObject m_canvas;

    int m_answer;

    Game m_game;

    // Use this for initialization
    void Start ()
    {
        m_game = FindObjectOfType<Game>();

        Reset();
	}

    public void Reset()
    {
        m_button1.SetActive(false);
        m_button2.SetActive(false);
        m_button3.SetActive(false);
        m_button4.SetActive(false);
        m_canvas.SetActive(false);
        
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
                m_answerText1.GetComponent<Text>().text = text;
                m_button1.SetActive(true);
                break;
            case 2:
                m_answerText1.GetComponent<Text>().text = text;
                m_button2.SetActive(true);
                break;
            case 3:
                m_answerText1.GetComponent<Text>().text = text;
                m_button3.SetActive(true);
                break;
            case 4:
                m_answerText1.GetComponent<Text>().text = text;
                m_button4.SetActive(true);
                break;
        }
    }

    public void SetAnswer(int ans)
    {
        m_answer = ans;
    }

    public void StartQuiz()
    {
        m_canvas.SetActive(true);

        

    }
    public void EndQuiz()
    {
        m_canvas.SetActive(false);
        m_game.AllowMovement();
    }

    public void ChooseAnswer(int i)
    {
        if(i==m_answer)
        {
            EndQuiz();
        }
    }
}
