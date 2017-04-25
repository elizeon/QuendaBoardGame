using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CrossroadsNode : Node
{

    new void Start()
    {
        base.Start();
        type = NodeType.crossroads;
    }

    public override void PerformAction()
    {
        


        StartCoroutine("CrossroadsChoice",true);
        // Create dialog box L/R
        // Wait for response
        // Move player to starting square of next path
        // Change current path to next path
    }

    [SerializeField]
    private int m_leftChoiceIndex=0;
    [SerializeField]
    private int m_rightChoiceIndex=0;



    IEnumerator CrossroadsChoice(bool wait)
    {
        Debug.Log("Which way do you want to go? L/R");
        while (wait)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Chose left path.");
              
                game.SetCurrentPathAtStart(m_leftChoiceIndex);
                game.AllowMovement();

                wait = false;
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Chose right path.");
                game.SetCurrentPathAtStart(m_rightChoiceIndex);
                game.AllowMovement();

                wait = false;
            }
            yield return null;
        }
    }
    
}

