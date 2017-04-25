using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizNode : Node
{


    /// <summary>
    /// Which quiz is given.
    /// </summary>
    public enum QuizType { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p };

    public QuizType m_quizType;
    public override void PerformAction()
    {
        QuizScreen quizScreen = game.quizScreen.GetComponent<QuizScreen>();


        switch (m_quizType)
        {
            case QuizType.a:
                Debug.Log("Quenda quiz A!");
                quizScreen.SetQuestion("???");
                quizScreen.SetQuizNode(1, "Answer");
                quizScreen.SetQuizNode(2, "Answer2");
                quizScreen.SetQuizNode(3, "Answer3");
                quizScreen.SetCorrectMessage("Correct!");
                quizScreen.SetIncorrectMessage("Incorrect.");
                quizScreen.SetHint("The answer is 1!");

                quizScreen.SetAnswer(1);
                break;


        }

        quizScreen.StartQuiz();

    }

}
