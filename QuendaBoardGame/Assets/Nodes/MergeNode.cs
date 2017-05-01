using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MergeNode : Node
{
    new void Start()
    {
        base.Start();
        type = NodeType.merge;
    }

    public override void PerformAction()
    {
        if(game.tilesToMove>=0)
        {

            Debug.Log("Merging paths move>=0.");
            game.SetCurrentPathAtStart(m_newPathIndex);
        }
        else
        {

            Debug.Log("Merging paths move<0.");
            game.SetCurrentPathAtEnd(m_prevPathIndex);
        }

    }

    
    [SerializeField]
    int m_newPathIndex;
    [SerializeField]
    int m_prevPathIndex;
    public int newPathIndex { get { return m_newPathIndex; } }
    public int prevPathIndex { get { return m_prevPathIndex; } }

}
