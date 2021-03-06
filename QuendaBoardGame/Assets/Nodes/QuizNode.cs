﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/*
 * Board game node for quizzes
 * Written by Elizabeth Haynes
 * 
 * */
public class QuizNode : Node
{

    public override void Start()
    {

        base.Start();
        type = Node.NodeType.quiz;
    }
    /// <summary>
    /// What quiz starts when the player lands on this tile?
    /// </summary>
    public int m_quizIndex = -1;
    public override void PerformAction()
    {
        if (m_quizIndex == -1)
        {
            m_quizIndex = Random.Range(0,game.quizzes.Count);
        }
            

        QuizScreen quizScreen = game.quizScreen.GetComponent<QuizScreen>();

        SetupQuizScreen(quizScreen, game.quizzes[m_quizIndex]);
        
        quizScreen.StartQuiz();

    }

    void SetupQuizScreen(QuizScreen quizScreen, Quiz quiz)
    {
        quizScreen.SetQuestion(quiz.question);
        if (quiz.quizNode1 != "")
        {
            quizScreen.SetQuizNode(1, quiz.quizNode1);
        }
        if (quiz.quizNode2 != "")
        {
            quizScreen.SetQuizNode(2, quiz.quizNode2);
        }
        if (quiz.quizNode3 != "")
        {
            quizScreen.SetQuizNode(3, quiz.quizNode3);
        }
        if (quiz.quizNode4 != "")
        {
            quizScreen.SetQuizNode(4, quiz.quizNode4);
        }

        quizScreen.SetQuiz(quiz);

        quizScreen.SetCorrectMessage(quiz.correctMessage);
        quizScreen.SetIncorrectMessage(quiz.incorrectMessage);
        quizScreen.SetHint(quiz.hint);
        quizScreen.SetAnswer(quiz.answer);
    }

}
