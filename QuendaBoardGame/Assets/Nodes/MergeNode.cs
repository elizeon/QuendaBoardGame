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
        Debug.Log("Merging paths.");
        game.SetCurrentPathAtStart(m_newPathIndex);
    }

    
    [SerializeField]
    int m_newPathIndex;
    public int newPathIndex { get { return m_newPathIndex; } }
}
