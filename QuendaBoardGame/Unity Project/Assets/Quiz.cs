using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Quiz data
 * 
 * Written by Elizabeth Haynes
 * 
 * */
[System.Serializable]
public class Quiz
{

    public string question = "";
    public string quizNode1 = "";
    public string quizNode2 = "";
    public string quizNode3 = "";
    public string quizNode4 = "";
    public string correctMessage = "";
    public string incorrectMessage = "";
    public string hint = "";
    public int answer = 1;
    public string name = "";
}
