using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : Node
{

    public override void PerformAction()
    {
        game.StopMovement();
    }
}
