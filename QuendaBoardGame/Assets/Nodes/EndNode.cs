using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Final node for board game
 * By Elizabeth Haynes
 * */
public class EndNode : Node {

    public override void Start()
    {
        base.Start();
        type = Node.NodeType.end;
    }
    public override void PerformAction()
    {
        switch(game.currentLevel.id)
        {
            case 0:

                game.DisallowStartMovement();
                game.ShowSaveMsgBox();
                game.ReadyMoveToLevel(1);
                game.SaveResults();
                break;
            case 1:
                game.messageBox.DisplayMessageBox("Congratulations - you made it through the area to your new home! You have won the game.", false);
                game.ReadyMoveToLevel(2);

                break;

        }

    }
}
