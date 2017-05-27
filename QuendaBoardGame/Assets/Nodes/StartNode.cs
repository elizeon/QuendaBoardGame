using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Starting node of board game
 * Written by Elizabeth Haynes
 * 
 * */

public class StartNode : Node
{

    public override void PerformAction()
    {
        game.StopMovement();
        game.AllowStartMovement();
    }
}
