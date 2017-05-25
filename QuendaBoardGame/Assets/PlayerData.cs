using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public string m_name;
    public List<bool> m_results = new List<bool>();

    public void AddResult(bool newResult)
    {
        m_results.Add(newResult);
    }

    public int GetCount()
    {
        return m_results.Count;
    }
}
