using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNode : Node {
    public override void PerformAction()
    {
        game.DisallowStartMovement();
        game.messageBox.DisplayMessageBox("You completed the level!",false);
        game.SaveResults();
    }
}
